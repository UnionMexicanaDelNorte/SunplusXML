//
//  TarjetaTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 03/02/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "TarjetaTableViewController.h"
#import "AppDelegate.h"
@interface TarjetaTableViewController ()

@end

@implementation TarjetaTableViewController
@synthesize tarjeta=_tarjeta,load=_load;
- (void)viewDidLoad {
    [super viewDidLoad];
    _tarjeta=[[NSMutableArray alloc] init];
    _subtarjeta=[[NSMutableArray alloc] init];
    if(_tarjeta!=nil)
    {
        [_tarjeta removeAllObjects];
    }
    if(_subtarjeta!=nil)
    {
        [_subtarjeta removeAllObjects];
    }
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    NSString *periodo = [defaults valueForKey:@"PERIOD"];
    NSRange range;
    range.location=5;
    range.length=2;
    NSString *fecha = [NSString stringWithFormat:@"%@/%@",[periodo substringToIndex:4],[periodo substringWithRange:range]];
    self.title=fecha;
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
}
-(void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        LoadingView *load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            
            NSString *urlString = @"http://myreport.unionnorte.org/myreport.php";
            NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
            NSString *periodo = [defaults valueForKey:@"PERIOD"];
            NSRange range;
            range.location=5;
            range.length=2;
            NSString *fecha = [NSString stringWithFormat:@"%@/%@",[periodo substringToIndex:4],[periodo substringWithRange:range]];
            
            
            NSString *post = [NSString stringWithFormat:@"servicio=empresarial&accion=obtenTablaMovimientos&idPersona=%@&fecha=%@",[defaults valueForKey:@"idPersona"],fecha];
            
            NSData *postData = [post dataUsingEncoding:NSASCIIStringEncoding allowLossyConversion:YES];
            
            NSString *postLength = [NSString stringWithFormat:@"%d", (int)[postData length]];
            
            
            
            NSCharacterSet *set = [NSCharacterSet URLQueryAllowedCharacterSet];
            
            NSString* encodedUrl = [urlString stringByAddingPercentEncodingWithAllowedCharacters:
                                    set];
            
            NSURL * url = [NSURL URLWithString:encodedUrl];
            NSMutableURLRequest * urlRequest = [NSMutableURLRequest requestWithURL:url];
            [urlRequest setHTTPMethod:@"POST"];//GET
            [urlRequest setValue:postLength forHTTPHeaderField:@"Content-Length"];
            [urlRequest setValue:@"application/x-www-form-urlencoded" forHTTPHeaderField:@"Content-Type"];
            [urlRequest setHTTPBody:postData];
            NSURLSessionDataTask * dataTask =[defaultSession dataTaskWithRequest:urlRequest
                                                               completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
                                                                   if(error == nil)
                                                                   {
                                                                       NSError* error;
                                                                       NSDictionary* json = [NSJSONSerialization
                                                                                             JSONObjectWithData:data
                                                                                             options:kNilOptions
                                                                                             error:&error];
                                                                       
                                                                       
                                                                       int success = [[json objectForKey:@"success"] intValue];
                                                                       if(success==1)
                                                                       {
                                                                           NSArray *mov =[json objectForKey:@"mov"];
                                                                           int i=0;
                                                                           for(i=0;i<[mov count];i++)
                                                                           {
                                                                               NSString *fecha=[[mov objectAtIndex:i] valueForKey:@"fecha"];
                                                                               
                                                                               
                                                                               NSString *tipo=[[mov objectAtIndex:i] valueForKey:@"tipo"];
                                                                               
                                                                               NSString *empresa=[[mov objectAtIndex:i] valueForKey:@"empresa"];
                                                                               
                                                                               NSString *lugar=[[mov objectAtIndex:i] valueForKey:@"lugar"];
                                                                               
                                                                               NSString *refaut=[[mov objectAtIndex:i] valueForKey:@"refaut"];
                                                                               
                                                                             float cantidad=[[[mov objectAtIndex:i] valueForKey:@"cantidad"] floatValue];
                                                                               
                                                                               float saldo=[[[mov objectAtIndex:i] valueForKey:@"saldo"] floatValue];
                                                                               
                                                                            
                                                                               
                                                                               // float ref =[[json objectForKey:@"ref"] floatValue];
                                                                               
                                                                              
                                                                               
                                                                               NSString *titulo = [NSString stringWithFormat:@"%@ $%.2f %@",fecha,cantidad,tipo];
                                                                               
                                                                               NSString *subtitulo = [NSString stringWithFormat:@"$%.2f %@ %@ %@",saldo,empresa,lugar,refaut];
                                                                               [_tarjeta addObject:titulo];
                                                                               [_subtarjeta addObject:subtitulo];
                                                                           }
                                                                           
                                                                         
                                                                           [self.tableView reloadData];

                                                                           
                                                                           
                                                                           
                                                                       }
                                                                       else
                                                                       {
                                                                           UIAlertController * view=   [UIAlertController alertControllerWithTitle:@"Mis Cuentas"
                                                                                                                                           message:@"No hay movimientos que mostrar"
                                                                                                                                    preferredStyle:UIAlertControllerStyleAlert];
                                                                           UIAlertAction* cancel = [UIAlertAction
                                                                                                    actionWithTitle:@"Aceptar"
                                                                                                    style:UIAlertActionStyleDefault
                                                                                                    handler:^(UIAlertAction * action)
                                                                                                    {
                                                                                                        [view dismissViewControllerAnimated:YES completion:nil];
                                                                                                    }];
                                                                           [view addAction:cancel];
                                                                           [self presentViewController:view animated:YES completion:nil];
                                                                       }
                                                                   }
                                                                   [load removeView];
                                                                   
                                                               }];
            
            [dataTask resume];
            
            dispatch_async(dispatch_get_main_queue(), ^{
                // Code to update the UI/send notifications based on the results of the background processing
                //            [_message show];
                
                
            });
            
        });
    }
    else
    {
        [self showNoHayInternet];
    }
}
-(void)showNoHayInternet
{
    UIAlertController * view=   [UIAlertController
                                 alertControllerWithTitle:@"Mis Cuentas"
                                 message:@"No hay una conexión disponible de internet, favor de conectarse a internet."
                                 preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction* cancel = [UIAlertAction
                             actionWithTitle:@"Aceptar"
                             style:UIAlertActionStyleDefault
                             handler:^(UIAlertAction * action)
                             {
                                 [view dismissViewControllerAnimated:YES completion:nil];
                             }];
    [view addAction:cancel];
    [self presentViewController:view animated:YES completion:nil];
}
- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark - Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _tarjeta.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"tarjetaCell" forIndexPath:indexPath];
    cell.textLabel.text=[_tarjeta objectAtIndex:indexPath.row];
    cell.detailTextLabel.text=[_subtarjeta objectAtIndex:indexPath.row];
    
    // Configure the cell...
    
    return cell;
}


/*
// Override to support conditional editing of the table view.
- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the specified item to be editable.
    return YES;
}
*/

/*
// Override to support editing the table view.
- (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath {
    if (editingStyle == UITableViewCellEditingStyleDelete) {
        // Delete the row from the data source
        [tableView deleteRowsAtIndexPaths:@[indexPath] withRowAnimation:UITableViewRowAnimationFade];
    } else if (editingStyle == UITableViewCellEditingStyleInsert) {
        // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view
    }   
}
*/

/*
// Override to support rearranging the table view.
- (void)tableView:(UITableView *)tableView moveRowAtIndexPath:(NSIndexPath *)fromIndexPath toIndexPath:(NSIndexPath *)toIndexPath {
}
*/

/*
// Override to support conditional rearranging of the table view.
- (BOOL)tableView:(UITableView *)tableView canMoveRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the item to be re-orderable.
    return YES;
}
*/

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
