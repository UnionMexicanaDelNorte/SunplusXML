//
//  BeneficiarioTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 28/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "BeneficiarioTableViewController.h"
#import "AppDelegate.h"
@interface BeneficiarioTableViewController ()

@end

@implementation BeneficiarioTableViewController
@synthesize diario=_diario,linea=_linea,beneficiarios=_beneficiarios,load=_load,searchController=_searchController,searchResults=_searchResults;
#pragma mark - UISearchBarDelegate

#pragma mark - cycle life

- (void)viewDidLoad {
    [super viewDidLoad];
    self.searchResults=nil;
    _beneficiarios = [[NSMutableArray alloc] init];
 
}

-(void)showNoHayInternet
{
    UIAlertController * view=   [UIAlertController
                                 alertControllerWithTitle:@"Mis cuentas"
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
-(void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        _load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
            
            
            NSString *urlString = [NSString stringWithFormat:@"%@&accion=17&argumento1=%@",[defaults valueForKey:@"url"],[defaults valueForKey:@"unidadSeleccionada"]];
            
            NSCharacterSet *set = [NSCharacterSet URLQueryAllowedCharacterSet];
            
            NSString* encodedUrl = [urlString stringByAddingPercentEncodingWithAllowedCharacters:
                                    set];
            
            NSURL * url = [NSURL URLWithString:encodedUrl];
            NSMutableURLRequest * urlRequest = [NSMutableURLRequest requestWithURL:url];
            [urlRequest setHTTPMethod:@"GET"];//GET
            
            
            
            
            NSURLSessionDataTask * dataTask =[defaultSession dataTaskWithRequest:urlRequest                                                               completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
                if(error == nil)
                {
                    NSError* error;
                    
                    NSDictionary* json = [NSJSONSerialization
                                          JSONObjectWithData:data
                                          options:NSJSONReadingMutableContainers
                                          error:&error];
                    
                    int success = [[json objectForKey:@"success"] intValue];
                    if(success==1)
                    {
                        _beneficiarios =[json objectForKey:@"beneficiarios"];
                        [self.tableView reloadData];
                        
                    }
                    else
                    {
                        
                            dispatch_async(dispatch_get_main_queue(), ^{
                                UIAlertController * view=   [UIAlertController
                                                             alertControllerWithTitle:@"Mis cuentas"
                                                             message:@"No existen beneficiarios o hubo un error."
                                                             preferredStyle:UIAlertControllerStyleAlert];
                                UIAlertAction* cancel = [UIAlertAction
                                                         actionWithTitle:@"Aceptar"
                                                         style:UIAlertActionStyleDefault
                                                         handler:^(UIAlertAction * action)
                                                         {
                                                             [view dismissViewControllerAnimated:YES completion:nil];
                                                             [self.navigationController popToRootViewControllerAnimated:YES];
                                                         }];
                                [view addAction:cancel];
                                [self presentViewController:view animated:YES completion:nil];
                                
                            });
                        
                    }
                }
                [_load removeView];
            }];
            
            [dataTask resume];
            
            
        });
        
    }
    else
    {
        [self showNoHayInternet];
    }
    
    
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
    
    if (self.searchResults!=nil)
    {
        return [self.searchResults count];
    }
    else{
        return [self.beneficiarios count];
    }
    return _beneficiarios.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"beneCell" forIndexPath:indexPath];
    if (self.searchResults!=nil)
    {
        cell.textLabel.text=@"al menos entro";//[[_beneficiarios objectAtIndex:indexPath.row] objectForKey:@"DESCRIPTN"];
    }
    else
    {
        cell.textLabel.text=[[_beneficiarios objectAtIndex:indexPath.row] objectForKey:@"DESCRIPTN"];
    }
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





@end
