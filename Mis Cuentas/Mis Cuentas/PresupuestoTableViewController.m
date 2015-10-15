//
//  PresupuestoTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 12/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import "PresupuestoTableViewController.h"
#import "AppDelegate.h"
@interface PresupuestoTableViewController ()

@end

@implementation PresupuestoTableViewController
@synthesize presupuesto=_presupuesto,detailPresupuesto=_detailPresupuesto,load=_load;
- (void)viewDidLoad {
    [super viewDidLoad];
    _presupuesto = [[NSMutableArray alloc] init];
    
    _detailPresupuesto = [[NSMutableArray alloc] init];
    
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
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
            NSString *WHO =[[NSUserDefaults standardUserDefaults] valueForKey:@"WHO"];
            NSString *PERIOD =[[NSUserDefaults standardUserDefaults] valueForKey:@"PERIOD"];
            NSString *urlYpuerto =[[NSUserDefaults standardUserDefaults] valueForKey:@"URLyPUERTO"];
            
            NSString *urlString =[NSString stringWithFormat:@"http://%@/?accion=5&argumento1=%@&argumento2=%@",urlYpuerto,WHO,PERIOD];
            
            
            NSString* encodedUrl = [urlString stringByAddingPercentEscapesUsingEncoding:
                                    NSUTF8StringEncoding];
            NSURL * url = [NSURL URLWithString:encodedUrl];
            NSMutableURLRequest * urlRequest = [NSMutableURLRequest requestWithURL:url];
            [urlRequest setHTTPMethod:@"POST"];//GET
            NSURLSessionDataTask * dataTask =[defaultSession dataTaskWithRequest:urlRequest                                                               completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
                NSLog(@"%@",[error description]);
                if(error == nil)
                {
                    NSError* error;
                    
                    NSDictionary* json = [NSJSONSerialization
                                          JSONObjectWithData:data
                                          options:NSJSONReadingMutableContainers
                                          error:&error];
                    NSLog(@"%@",[error description]);
                    
                    int success = [[json objectForKey:@"success"] intValue];
                    if(success==1)
                    {
                        NSArray *presupuestoAux =[json objectForKey:@"presupuesto"];
                        int i=0;
                        for(i=0;i<[presupuestoAux count];i++)
                        {
                            NSString *s=[[presupuestoAux objectAtIndex:i] valueForKey:@"DESCR"];
                            [_presupuesto addObject:s];
                            NSString *amount=[[presupuestoAux objectAtIndex:i] valueForKey:@"amount"];
                            NSString *gastado=[[presupuestoAux objectAtIndex:i] valueForKey:@"gastado"];
                            [_detailPresupuesto addObject:[NSString stringWithFormat:@"$%@   informado: $%@",amount,gastado]];
                        }
                        [self.tableView reloadData];
                        
                    }
                    else
                    {
                        if(success==-1)
                        {
                            dispatch_async(dispatch_get_main_queue(), ^{
                                UIAlertController * view=   [UIAlertController
                                                             alertControllerWithTitle:@"Mis cuentas"
                                                             message:@"No existe departamento asignado al WHO. Favor de reportarlo al contador del campo."
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
    return _presupuesto.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
 //   UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"presuCell" forIndexPath:indexPath];
    UITableViewCell *cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:[NSString stringWithFormat:@"%ld-%ld",(long)indexPath.section,(long)indexPath.row]];
    
    cell.textLabel.text = [_presupuesto objectAtIndex:indexPath.row];
    cell.detailTextLabel.text = [_detailPresupuesto objectAtIndex:indexPath.row];
    
    
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
