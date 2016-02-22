//
//  LoginViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 19/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "LoginViewController.h"
#import "LoadingView.h"
#import "AppDelegate.h"
@interface LoginViewController ()

@end

@implementation LoginViewController
@synthesize correo=_correo,pass=_pass,recordarme=_recordarme;

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
-(IBAction)entrarComoInvitado:(id)sender
{
    [[NSUserDefaults standardUserDefaults] setBool:NO forKey:@"AlreadyLogin"];
    [[NSUserDefaults standardUserDefaults] setInteger:11 forKey:@"idCampo"];
    [[NSUserDefaults standardUserDefaults] setInteger:2 forKey:@"idPersona"];
    [[NSUserDefaults standardUserDefaults] setObject:@"ERPRUEB01" forKey:@"ER"];
    [[NSUserDefaults standardUserDefaults] setObject:@"http://sunplus.redirectme.net:90/?" forKey:@"url"];
    [[NSUserDefaults standardUserDefaults] setObject:@"PRUEBA" forKey:@"nombre"];
    [[NSUserDefaults standardUserDefaults] setObject:@"2016001" forKey:@"PERIOD"];
    [[NSUserDefaults standardUserDefaults] setObject:@"SUTECNO01" forKey:@"FNCT"];
    [[NSUserDefaults standardUserDefaults] setInteger:0 forKey:@"tipoDeUsuario"];
    NSDictionary *dic = @{
                        @"nombre" : @"CEA",
                        @"nombreAmigable" : @"UMN"
                        };
    NSArray *unidades= @[dic];
    [[NSUserDefaults standardUserDefaults] setObject:unidades forKey:@"unidades"];
    [[NSUserDefaults standardUserDefaults] setObject:@"CEA" forKey:@"unidadSeleccionada"];
    [[NSUserDefaults standardUserDefaults] synchronize];
    [self performSegueWithIdentifier:@"entra" sender:nil];
}
-(IBAction)entrar:(id)sender
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        LoadingView *load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            
            
            NSString *urlString = @"http://myreport.unionnorte.org/myreport.php";
            

            NSString *post = [[NSString stringWithFormat:@"servicio=login&accion=access&correo=%@&pass=%@&isMobile=1",[_correo text],[_pass text]] stringByReplacingOccurrencesOfString:@"'" withString:@""];
            
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
                    NSInteger idCampo = [[json objectForKey:@"idCampo"] integerValue];
                    NSInteger idPersona = [[json objectForKey:@"idPersona"] integerValue];
                    NSString *ER = [json objectForKey:@"ER"];
                    NSString *nombre = [json objectForKey:@"nombre"];
                    NSString *PERIOD = [json objectForKey:@"PERIOD"];
                    NSString *FNCT = [json objectForKey:@"FNCT"];
                    int tipoDeUsuario = (int)[[json objectForKey:@"tipoDeUsuario"] integerValue];
                    [[NSUserDefaults standardUserDefaults] setInteger:tipoDeUsuario forKey:@"tipoDeUsuario"];
                    NSString *url = [json objectForKey:@"url"];
                    if([_recordarme isOn])
                    {
                        [[NSUserDefaults standardUserDefaults] setBool:YES forKey:@"AlreadyLogin"];
                        [[NSUserDefaults standardUserDefaults] setObject:[_correo text] forKey:@"correo"];
                        [[NSUserDefaults standardUserDefaults] setObject:[[_pass text] stringByReplacingOccurrencesOfString:@"'" withString:@""] forKey:@"pass"];
                        
                    }
                    else
                    {
                        [[NSUserDefaults standardUserDefaults] setBool:NO forKey:@"AlreadyLogin"];
                    }
                    NSArray *unidades = [json objectForKey:@"unidades"];
                    NSMutableArray *unidadesMeter = [[NSMutableArray alloc] initWithCapacity:unidades.count];
                    int j;
                    for(j=0;j<unidades.count;j++)
                    {
                        NSMutableDictionary *dicNuevo = [[NSMutableDictionary alloc] init];
                        NSMutableDictionary *dic = [unidades objectAtIndex:j];
                        [dicNuevo setObject:[dic objectForKey:@"nombre"] forKey:@"nombre"];
                        [dicNuevo setObject:[dic objectForKey:@"nombreAmigable"] forKey:@"nombreAmigable"];
                        [unidadesMeter addObject:dicNuevo];
                    }
                    
                    
                    [[NSUserDefaults standardUserDefaults] setObject:[unidadesMeter copy] forKey:@"unidades"];
                    [[NSUserDefaults standardUserDefaults] setObject:@"CEA" forKey:@"unidadSeleccionada"];
                    [[NSUserDefaults standardUserDefaults] setInteger:idCampo forKey:@"idCampo"];
                    [[NSUserDefaults standardUserDefaults] setInteger:idPersona forKey:@"idPersona"];
                    [[NSUserDefaults standardUserDefaults] setObject:ER forKey:@"ER"];
                    [[NSUserDefaults standardUserDefaults] setObject:url forKey:@"url"];
                    [[NSUserDefaults standardUserDefaults] setObject:FNCT forKey:@"FNCT"];
                    [[NSUserDefaults standardUserDefaults] setObject:nombre forKey:@"nombre"];
                    [[NSUserDefaults standardUserDefaults] setObject:PERIOD forKey:@"PERIOD"];
                    [[NSUserDefaults standardUserDefaults] synchronize];
                    [self performSegueWithIdentifier:@"entra" sender:nil];
                }
                else
                {
                    UIAlertController * view=   [UIAlertController alertControllerWithTitle:@"Mis Cuentas"
                        message:@"Contraseña incorrecta"
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
-(void)dismissKeyboard {
    [_correo resignFirstResponder];
    [_pass resignFirstResponder];
}


-(BOOL)textFieldShouldReturn:(UITextField*)textField
{
    if(textField==_correo)
    {
        [_pass becomeFirstResponder];
        [_correo resignFirstResponder];
        return YES;
    }
    else
    {
        [self entrar:nil];
    }   
    return NO; // We do not want UITextField to insert line-breaks.
}
- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    if([defaults valueForKey:@"AlreadyLogin"])
    {
        [_correo setText:[defaults objectForKey:@"correo"]];
        [_pass setText:[defaults objectForKey:@"pass"]];
    }
}

- (void)viewDidLoad {
    [super viewDidLoad];
    self.navigationController.navigationBar.backgroundColor = [UIColor colorWithRed:0.0f/255.0f green:121.0f/255.0f blue:132.0f/255.0f alpha:1.0f];
    [self.navigationController.navigationBar
     setTitleTextAttributes:@{NSForegroundColorAttributeName : [UIColor whiteColor]}];
    self.navigationController.navigationBar.tintColor = [UIColor whiteColor];
    
    self.navigationController.navigationBar.barTintColor = [UIColor colorWithRed:0.0f/255.0f green:121.0f/255.0f blue:132.0f/255.0f alpha:1.0f];
    UITapGestureRecognizer *tap = [[UITapGestureRecognizer alloc]
                                   initWithTarget:self
                                   action:@selector(dismissKeyboard)];
    
    [self.view addGestureRecognizer:tap];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
