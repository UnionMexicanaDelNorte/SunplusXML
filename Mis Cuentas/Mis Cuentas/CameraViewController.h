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
    
    NSMutableArray *idConceptoGastos;
    NSMutableArray *idconceptoRecursos;
}
@property (nonatomic,strong) IBOutlet UIView *viewPreview;
@property (nonatomic,strong) IBOutlet UILabel *lblStatus;
@property (nonatomic,strong) IBOutlet UITextField *cantidadText;
@property (nonatomic,strong) IBOutlet UIPickerView *pickerGastos;
@property (nonatomic,strong) IBOutlet UIPickerView *pickerRecurso;
-(IBAction)guardaMovimiento:(id)sender;
@end
