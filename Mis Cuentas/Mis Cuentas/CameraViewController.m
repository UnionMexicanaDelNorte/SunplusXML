//
//  CameraViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import "CameraViewController.h"
#import "AppDelegate.h"
@interface CameraViewController ()
@property (nonatomic, strong) AVCaptureSession *captureSession;
@property (nonatomic, strong) AVCaptureVideoPreviewLayer *videoPreviewLayer;
@property (nonatomic, strong) AVAudioPlayer *audioPlayer;
@property (nonatomic) BOOL isReading;

-(BOOL)startReading;
-(void)stopReading;

@end

@implementation CameraViewController
@synthesize viewPreview=_viewPreview,lblStatus=_lblStatus;
- (void)viewDidLoad {
    [super viewDidLoad];
    _captureSession = nil;
    
    // Set the initial value of the flag to NO.
    _isReading = NO;
    gastos = [[NSMutableArray alloc] init];
    recursos = [[NSMutableArray alloc] init];
    idConceptoGastos = [[NSMutableArray alloc] init];
    idconceptoRecursos = [[NSMutableArray alloc] init];
    
    UITapGestureRecognizer *tap = [[UITapGestureRecognizer alloc]
                                   initWithTarget:self
                                   action:@selector(dismissKeyboard)];
    
    [self.view addGestureRecognizer:tap];

    
    
    
    [self startReading];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}
-(void)dismissKeyboard {
    [_cantidadText resignFirstResponder];
}
-(BOOL)textFieldShouldReturn:(UITextField*)textField
{
    return YES;
}
/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

-(IBAction)guardaMovimiento:(id)sender
{
    double cantidad = [_cantidadText.text doubleValue];
    int indexGasto = (int)[_pickerGastos selectedRowInComponent:0];
    int idConceptoGastoInt = [[idConceptoGastos objectAtIndex:indexGasto] intValue];

    int indexRecurso = (int)[_pickerRecurso selectedRowInComponent:0];
    int idConceptoRecursoInt = [[idconceptoRecursos objectAtIndex:indexRecurso] intValue];

    
    
    if(cantidad>maximo)
    {
        UIAlertController * view=   [UIAlertController
                                     alertControllerWithTitle:@"Mis cuentas"
                                     message:[NSString stringWithFormat:@"La cantidad no puede ser mayor a: $%f",maximo]
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
        return;
    }
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
            NSString *WHO =[[NSUserDefaults standardUserDefaults] valueForKey:@"WHO"];
            NSString *urlYpuerto =[[NSUserDefaults standardUserDefaults] valueForKey:@"URLyPUERTO"];
            
            NSString *urlString =[NSString stringWithFormat:@"http://%@/?accion=3&argumento1=%@&argumento2=%@&argumento3=%.02f&argumento4=%d&argumento5=%d",urlYpuerto,WHO,UUID,cantidad,idConceptoGastoInt,idConceptoRecursoInt];
            
            
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
                                          options:kNilOptions
                                          error:&error];
                    int success = [[json objectForKey:@"success"] intValue];
                    if(success==1)
                    {
                       //me regreso
                        dispatch_async(dispatch_get_main_queue(), ^{
                            UIAlertController * view=   [UIAlertController
                                                         alertControllerWithTitle:@"Mis cuentas"
                                                         message:@"Factura informada"
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
                    else
                    {
                        
                            dispatch_async(dispatch_get_main_queue(), ^{
                                UIAlertController * view=   [UIAlertController
                                                             alertControllerWithTitle:@"Mis cuentas"
                                                             message:@"Hubo un error"
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
                    }
                    
                    
                }
                [load removeView];
            }];
            
            [dataTask resume];
            
            
        });
        
    }
    else
    {
        [self showNoHayInternet];
    }
    
    
}
#pragma mark - Private method implementation

- (BOOL)startReading {
    NSError *error;
    
    // Get an instance of the AVCaptureDevice class to initialize a device object and provide the video
    // as the media type parameter.
    AVCaptureDevice *captureDevice = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
    
    // Get an instance of the AVCaptureDeviceInput class using the previous device object.
    AVCaptureDeviceInput *input = [AVCaptureDeviceInput deviceInputWithDevice:captureDevice error:&error];
    
    if (!input) {
        // If any error occurs, simply log the description of it and don't continue any more.
        NSLog(@"%@", [error localizedDescription]);
        return NO;
    }
    
    // Initialize the captureSession object.
    _captureSession = [[AVCaptureSession alloc] init];
    // Set the input device on the capture session.
    [_captureSession addInput:input];
    
    
    // Initialize a AVCaptureMetadataOutput object and set it as the output device to the capture session.
    AVCaptureMetadataOutput *captureMetadataOutput = [[AVCaptureMetadataOutput alloc] init];
    [_captureSession addOutput:captureMetadataOutput];
    
    // Create a new serial dispatch queue.
    dispatch_queue_t dispatchQueue;
    dispatchQueue = dispatch_queue_create("myQueue", NULL);
    [captureMetadataOutput setMetadataObjectsDelegate:self queue:dispatchQueue];
    [captureMetadataOutput setMetadataObjectTypes:[NSArray arrayWithObject:AVMetadataObjectTypeQRCode]];
    
    // Initialize the video preview layer and add it as a sublayer to the viewPreview view's layer.
    _videoPreviewLayer = [[AVCaptureVideoPreviewLayer alloc] initWithSession:_captureSession];
    [_videoPreviewLayer setVideoGravity:AVLayerVideoGravityResizeAspectFill];
    [_videoPreviewLayer setFrame:_viewPreview.layer.bounds];
    [_viewPreview.layer addSublayer:_videoPreviewLayer];
    
    
    // Start video capture.
    [_captureSession startRunning];
    
    return YES;
}

//- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView

//- (NSInteger)pickerView:(UIPickerView *)pickerView
//numberOfRowsInComponent:(NSInteger)component

- (NSInteger)pickerView:(UIPickerView *)pickerView
numberOfRowsInComponent:(NSInteger)component
{
    if(pickerView==_pickerGastos)
    {
       return gastos.count;
    }
    return recursos.count;
}
- (NSInteger)numberOfComponentsInPickerView:
(UIPickerView *)pickerView
{
    if(gastos.count==0)
    {
        return 0;
    }
    return 1;
}

- (NSString *)pickerView:(UIPickerView *)pickerView
             titleForRow:(NSInteger)row
            forComponent:(NSInteger)component
{
    if(pickerView==_pickerGastos)
    {
        return [gastos objectAtIndex:row];
    }
    return [recursos objectAtIndex:row];
}

-(void)stopReading{
    // Stop video capture and make the capture session object nil.
    [_captureSession stopRunning];
    _captureSession = nil;
    
    // Remove the video preview layer from the viewPreview view's layer.
    [_videoPreviewLayer removeFromSuperlayer];
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if (app.hasInternet)
    {
        load = [LoadingView loadingViewInView:self.view];
        dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
            NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
             NSString *WHO =[[NSUserDefaults standardUserDefaults] valueForKey:@"WHO"];
            NSString *urlYpuerto =[[NSUserDefaults standardUserDefaults] valueForKey:@"URLyPUERTO"];
            
            NSString *urlString =[NSString stringWithFormat:@"http://%@/?accion=2&argumento1=%@&argumento2=%@",urlYpuerto,UUID,WHO];
            
            
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
                        double cantidad = [[json objectForKey:@"cantidad"] doubleValue];
                        maximo = cantidad;
                        _cantidadText.text = [NSString stringWithFormat:@"%.02f",cantidad];
                        NSArray *gastosAux =[json objectForKey:@"gastos"];
                        int i=0;
                        for(i=0;i<[gastosAux count];i++)
                        {
                            NSString *s=[[gastosAux objectAtIndex:i] valueForKey:@"nombre"];
                            [gastos addObject:s];
                            NSString *idCs=[[gastosAux objectAtIndex:i] valueForKey:@"idConcepto"];
                            [idConceptoGastos addObject:idCs];
                        }
                            
                        
                        NSArray *recursosAux =[json objectForKey:@"recursos"];
                        for(i=0;i<[recursosAux count];i++)
                        {
                            NSString *s=[[recursosAux objectAtIndex:i] valueForKey:@"nombre"];
                            [recursos addObject:s];
                            NSString *idCs=[[recursosAux objectAtIndex:i] valueForKey:@"idConcepto"];
                            [idconceptoRecursos addObject:idCs];
                        }
                        [_pickerGastos reloadAllComponents];
                        [_pickerRecurso reloadAllComponents];

                        
                        
                        
                    }
                    else
                    {
                        if(success==0)
                        {
                            dispatch_async(dispatch_get_main_queue(), ^{
                                UIAlertController * view=   [UIAlertController
                                                             alertControllerWithTitle:@"Mis cuentas"
                                                             message:@"Factura cancelada"
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
                        else
                        {
                            if(success==-1)
                            {
                                dispatch_async(dispatch_get_main_queue(), ^{
                                    UIAlertController * view=   [UIAlertController
                                                                 alertControllerWithTitle:@"Mis cuentas"
                                                                 message:@"Factura  no existente en nuestra base de datos, si la factura fue emitida hace 72 horas o menos, debe de esperar, de lo contrario favor de reportarlo al administrador."
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
                            else
                            {
                                if(success==-2)
                                {
                                    dispatch_async(dispatch_get_main_queue(), ^{
                                        UIAlertController * view=   [UIAlertController
                                                                     alertControllerWithTitle:@"Mis cuentas"
                                                                     message:@"Factura ya informada"
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
                    }
                    
                    
                }
                [load removeView];
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
#pragma mark - AVCaptureMetadataOutputObjectsDelegate method implementation

-(void)captureOutput:(AVCaptureOutput *)captureOutput didOutputMetadataObjects:(NSArray *)metadataObjects fromConnection:(AVCaptureConnection *)connection{
    
    // Check if the metadataObjects array is not nil and it contains at least one object.
    if (metadataObjects != nil && [metadataObjects count] > 0) {
        // Get the metadata object.
        AVMetadataMachineReadableCodeObject *metadataObj = [metadataObjects objectAtIndex:0];
        if ([[metadataObj type] isEqualToString:AVMetadataObjectTypeQRCode]) {
            // If the found metadata is equal to the QR code metadata then update the status label's text,
            // stop reading and change the bar button item's title and the flag's value.
            // Everything is done on the main thread.
            
            NSString *todo =[metadataObj stringValue];
            NSRange range = [todo rangeOfString:@"&id="];
           UUID = [[todo substringFromIndex:range.location+range.length] uppercaseString];
            
            
            
           /* [_lblStatus performSelectorOnMainThread:@selector(setText:) withObject:UUID waitUntilDone:NO];
            */
            [self performSelectorOnMainThread:@selector(stopReading) withObject:nil waitUntilDone:NO];
          //  [_bbitemStart performSelectorOnMainThread:@selector(setTitle:) withObject:@"Start!" waitUntilDone:NO];
            
            _isReading = NO;
            
            // If the audio player is not nil, then play the sound effect.
           // if (_audioPlayer) {
             //   [_audioPlayer play];
           // }
        }
    }
    
    
}
@end
