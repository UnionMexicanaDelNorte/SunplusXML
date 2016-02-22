//
//  CameraViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>
#import "LoadingView.h"
@interface CameraViewController : UIViewController  <UIPickerViewDataSource,AVCaptureMetadataOutputObjectsDelegate,UITextFieldDelegate,UIPickerViewDelegate>
{
    LoadingView *load;
    NSString *UUID;
    double maximo;
    NSMutableArray *gastos;
    NSMutableArray *recursos;
    float cantidadEnLosTemporales;
    NSMutableArray *idconceptoRecursos;
}
@property BOOL deboPonerCamara;
@property (nonatomic,strong) NSString *etiqueta;
@property (nonatomic,strong) NSString *diario;
@property (nonatomic,strong) NSString *linea;
@property (nonatomic,strong) NSString *fechaSeleccionada;
@property (nonatomic,strong) IBOutlet UIView *viewPreview;
@property (nonatomic,strong) IBOutlet UILabel *lblStatus;
@property (nonatomic,strong) IBOutlet UITextField *descripcion;
@property (nonatomic,strong) IBOutlet UILabel *lblFactura;
@property (nonatomic,strong) IBOutlet UIBarButtonItem *informar;
@property (nonatomic,strong) IBOutlet UITextField *cantidadText;
@property (nonatomic,strong) IBOutlet UIPickerView *pickerRecurso;
-(IBAction)guardaMovimiento:(id)sender;
@end
