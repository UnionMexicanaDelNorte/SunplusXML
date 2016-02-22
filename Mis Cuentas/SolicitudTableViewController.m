//
//  SolicitudTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 28/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "SolicitudTableViewController.h"
#import "AppDelegate.h"
#import "BeneficiarioTableViewController.h"
@interface SolicitudTableViewController ()

@end

@implementation SolicitudTableViewController
@synthesize presupuesto=_presupuesto,detailPresupuesto=_detailPresupuesto,load=_load,diarioLinea=_diarioLinea;

- (void)viewDidLoad {
    [super viewDidLoad];
    _presupuesto = [[NSMutableArray alloc] init];
    
    _detailPresupuesto = [[NSMutableArray alloc] init];
    
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
        _load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
            
            
            NSString *urlString = [NSString stringWithFormat:@"%@&accion=5&argumento1=%@&argumento2=%@&argumento3=%@&argumento4=%@",[defaults valueForKey:@"url"],[defaults valueForKey:@"ER"],[defaults valueForKey:@"PERIOD"],[defaults valueForKey:@"unidadSeleccionada"],[defaults valueForKey:@"FNCT"]];
            
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
                        NSArray *presupuestoAux =[json objectForKey:@"presupuesto"];
                        if([presupuestoAux count]==0)
                        {
                            dispatch_async(dispatch_get_main_queue(), ^{
                                UIAlertController * view=   [UIAlertController
                                                             alertControllerWithTitle:@"Mis cuentas"
                                                             message:@"No existe presupuesto para ese periodo."
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
                                
                            });
                             [_load removeView];
                            return;
                        }
                        int i=0;
                        for(i=0;i<[presupuestoAux count];i++)
                        {
                            NSString *s=[[presupuestoAux objectAtIndex:i] valueForKey:@"DESCRIPTN"];
                            [_presupuesto addObject:s];
                            //    NSString *amount=[[presupuestoAux objectAtIndex:i] valueForKey:@"amount"];
                            //  NSString *gastado=[[presupuestoAux objectAtIndex:i] valueForKey:@"gastado"];
                            NSString *diario=[[presupuestoAux objectAtIndex:i] valueForKey:@"JRNAL_NO"];
                            NSString *linea=[[presupuestoAux objectAtIndex:i] valueForKey:@"JRNAL_LINE"];
                            [_diarioLinea addObject:[NSString stringWithFormat:@"%@-%@",diario,linea]];
                            //[_detailPresupuesto addObject:[NSString stringWithFormat:@"$%@   informado: $%@",amount,gastado]];
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
                if(_load!=nil)
                {
                    [_load removeView];
                }
            }];
            
            [dataTask resume];
            
            
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
 UITableViewCell *cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:@"solicitudCell"];
 
 cell.textLabel.text = [_presupuesto objectAtIndex:indexPath.row];
 cell.detailTextLabel.text =@"";// [_detailPresupuesto objectAtIndex:indexPath.row];
    [cell setAccessoryType:UITableViewCellAccessoryDisclosureIndicator];
 
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


#pragma mark - Table view delegate

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    diarioLineaS=[_diarioLinea objectAtIndex:indexPath.row];
    [self performSegueWithIdentifier:@"irAlBeneficiario" sender:nil];
}

#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    if ([[segue identifier] isEqualToString:@"irAlBeneficiario"])
    {
        BeneficiarioTableViewController *per = (BeneficiarioTableViewController *)[segue destinationViewController];
        NSArray *arrayA = [diarioLineaS componentsSeparatedByString:@"-"];
        [per setDiario:[arrayA objectAtIndex:0]];
        [per setLinea:[arrayA objectAtIndex:1]];
    }
}


@end
