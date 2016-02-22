//
//  PeriodosTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 19/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "PeriodosTableViewController.h"
#import "AppDelegate.h"
@interface PeriodosTableViewController ()

@end

@implementation PeriodosTableViewController
@synthesize periodos=_periodos;
-(void)setPeriodos:(NSArray *)periodos
{
    _periodos=periodos;
    [self.tableView reloadData];
}
- (void)viewDidLoad {
    [super viewDidLoad];
    self.navigationController.navigationBar.backgroundColor = [UIColor colorWithRed:0.0f/255.0f green:121.0f/255.0f blue:132.0f/255.0f alpha:1.0f];
    [self.navigationController.navigationBar
     setTitleTextAttributes:@{NSForegroundColorAttributeName : [UIColor whiteColor]}];
    self.navigationController.navigationBar.tintColor = [UIColor whiteColor];
    
    self.navigationController.navigationBar.barTintColor = [UIColor colorWithRed:0.0f/255.0f green:121.0f/255.0f blue:132.0f/255.0f alpha:1.0f];
    
    
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
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
    return _periodos.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"periodoCell" forIndexPath:indexPath];
    NSString *currentCell =[NSString stringWithFormat:@"%ld",(long)[[_periodos objectAtIndex:indexPath.row] integerValue]];
    cell.textLabel.text=currentCell;
    NSString *currentPeriod = [[NSUserDefaults standardUserDefaults] objectForKey:@"PERIOD"];
    if([currentPeriod isEqualToString:currentCell])
    {
        [cell setAccessoryType:UITableViewCellAccessoryCheckmark];
    }
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSString *currentCell =[NSString stringWithFormat:@"%ld",(long)[[_periodos objectAtIndex:indexPath.row] integerValue]];
    [[NSUserDefaults standardUserDefaults] setObject:currentCell forKey:@"PERIOD"];
    [[NSUserDefaults standardUserDefaults]synchronize];
    //guarda periodo
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            
            NSString *urlString = @"http://myreport.unionnorte.org/myreport.php";
            
            NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
            
            NSString *post = [NSString stringWithFormat:@"servicio=config&accion=cambiaPeriodo&idPersona=%@&PERIOD=%@", [defaults valueForKey:@"idPersona"],[defaults valueForKey:@"PERIOD"]];
            
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
                                                                       NSDictionary* json = [NSJSONSerialization JSONObjectWithData:data options:kNilOptions error:&error];
                                                                       int success = [[json objectForKey:@"success"] intValue];
                                                                       if(success==1)
                                                                       {
                                                                           if(load!=nil)
                                                                           {
                                                                               [load removeView];
                                                                           }
                                                                           [self dismissViewControllerAnimated:YES completion:nil];

                                                                           
                                                                       }
                                                                       else
                                                                       {
                                                                           UIAlertController * view=   [UIAlertController alertControllerWithTitle:@"Mis Cuentas"
                                                                                                                                           message:@"Error, no se pudo cambiar de periodo"
                                                                                                                                    preferredStyle:UIAlertControllerStyleAlert];
                                                                           UIAlertAction* cancel = [UIAlertAction actionWithTitle:@"Aceptar"
                                                                                                                            style:UIAlertActionStyleDefault
                                                                                                                          handler:^(UIAlertAction * action)
                                                                                                    {
                                                                                                        [view dismissViewControllerAnimated:YES completion:nil];
                                                                                                    }];
                                                                           [view addAction:cancel];
                                                                           [self presentViewController:view animated:YES completion:nil];
                                                                       }
                                                                   }
                                                                   if(load!=nil)
                                                                   {
                                                                       [load removeView];
                                                                   }
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
