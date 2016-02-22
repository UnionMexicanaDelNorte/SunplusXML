//
//  MasterViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import "MasterViewController.h"
#import "AppDelegate.h"
#import "PeriodosTableViewController.h"
@interface MasterViewController ()

@property NSMutableArray *objects;
@end

@implementation MasterViewController
@synthesize periodos=_periodos,barButton=_barButton;

#define IDIOM    UI_USER_INTERFACE_IDIOM()
#define IPAD     UIUserInterfaceIdiomPad

-(void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    [_periodos removeAllObjects];
    self.title=[NSString stringWithFormat:@"Mis cuentas - %@",[[NSUserDefaults standardUserDefaults] objectForKey:@"PERIOD"]];
    
}
- (void)viewDidLoad {
    [super viewDidLoad];
    
    _periodos = [[NSMutableArray alloc] init];
    
    
    self.title=[NSString stringWithFormat:@"Mis cuentas - %@",[[NSUserDefaults standardUserDefaults] objectForKey:@"PERIOD"]];
    
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    UIApplication *application = [UIApplication sharedApplication];
    if (app.firstRun)
    {
        
        UIUserNotificationType userNotificationTypes = (UIUserNotificationTypeAlert |
                                                        UIUserNotificationTypeBadge |
                                                        UIUserNotificationTypeSound);
        UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:userNotificationTypes
                                                                                 categories:nil];
        [application registerUserNotificationSettings:settings];
        [application registerForRemoteNotifications];
    }
    
   
}

- (void)viewWillAppear:(BOOL)animated {
    self.clearsSelectionOnViewWillAppear = self.splitViewController.isCollapsed;
    [super viewWillAppear:animated];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


#pragma mark - Segues

- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
   
    if ([[segue identifier] isEqualToString:@"vesMenuPeriodos"])
    {
        PeriodosTableViewController *per = (PeriodosTableViewController *)[[segue destinationViewController] topViewController];
        [per setPeriodos:_periodos];
        

    }
}

#pragma mark - Table View

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return 2;
}

-(void)vesAPeriodos
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        _load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
            
            
            NSString *urlString = [NSString stringWithFormat:@"%@&accion=12&argumento1=%@&argumento2=%@",[defaults valueForKey:@"url"],[defaults valueForKey:@"ER"],[defaults valueForKey:@"unidadSeleccionada"]];
            
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
                        NSArray *presupuestoAux =[json objectForKey:@"periodos"];
                        int i=0;
                        for(i=0;i<[presupuestoAux count];i++)
                        {
                            NSString *s=[[presupuestoAux objectAtIndex:i] valueForKey:@"PERIOD"];
                            [_periodos addObject:s];
                        }
                        [self performSegueWithIdentifier:@"vesMenuPeriodos" sender:nil];
                        
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
-(void)vesAUnidadesDeNegocio
{
    [self performSegueWithIdentifier:@"vesAUnidadesDeNegocio" sender:nil];
}
-(IBAction)menuPeriodos:(id)sender
{
    
    UIAlertController * view=   [UIAlertController
                                 alertControllerWithTitle:@"Mis cuentas"
                                 message:@"Selecciona una opción"
                                 preferredStyle:UIAlertControllerStyleActionSheet];
    
    UIAlertAction* irAPeriodos = [UIAlertAction
                                actionWithTitle:@"Cambiar periodo"
                                style:UIAlertActionStyleDefault
                                handler:^(UIAlertAction * action)
                                {
                                    [view dismissViewControllerAnimated:YES completion:nil];
                                    [self vesAPeriodos];
                                }];
    
    UIAlertAction* irAUnidadesDeNegocio = [UIAlertAction
                               actionWithTitle:@"Cambiar de base de datos"
                               style:UIAlertActionStyleDefault
                               handler:^(UIAlertAction * action)
                               {
                                   [view dismissViewControllerAnimated:YES completion:nil];
                                   [self vesAUnidadesDeNegocio];
                               }];
    
    
    UIAlertAction* cancel = [UIAlertAction
                             actionWithTitle:@"Cancelar"
                             style:UIAlertActionStyleDefault
                             handler:^(UIAlertAction * action)
                             {
                                 [view dismissViewControllerAnimated:YES completion:nil];
                                 
                             }];
    
    
    [view addAction:irAPeriodos];
    [view addAction:irAUnidadesDeNegocio];
    [view addAction:cancel];
    
    if ( IDIOM == IPAD )
    {
        [view setModalPresentationStyle:UIModalPresentationPopover];
        view.popoverPresentationController.barButtonItem = _barButton;
        view.popoverPresentationController.sourceView = self.view;
    }
    
    [self presentViewController:view animated:YES completion:nil];

    
    
    
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
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell;
    switch (indexPath.row) {
        case 0:
            cell = [tableView dequeueReusableCellWithIdentifier:@"PresupuestoCell" forIndexPath:indexPath];
            break;
        case 1:
            cell = [tableView dequeueReusableCellWithIdentifier:@"InformeCell" forIndexPath:indexPath];
            break;
        case 2:
            cell = [tableView dequeueReusableCellWithIdentifier:@"SolicitudCell" forIndexPath:indexPath];
            break;
        case 3:
            cell = [tableView dequeueReusableCellWithIdentifier:@"PeriodoCell" forIndexPath:indexPath];
            NSString *aver =[[NSUserDefaults standardUserDefaults] valueForKey:@"PERIOD"];
            cell.textLabel.text = [NSString stringWithFormat:@"  Periodo actual: %@",aver];
            break;
    }
    
    
    return cell;
}
#pragma mark - Table view delegate

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    
}
- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the specified item to be editable.
    return NO;
}


@end
