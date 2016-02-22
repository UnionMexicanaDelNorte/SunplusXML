//
//  FechaViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 03/02/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface FechaViewController : UIViewController
{
    NSString *fechaSeleccionada;
}
@property (nonatomic,strong) IBOutlet UIDatePicker *date;
@property (nonatomic,strong) NSString *diario;
@property (nonatomic,strong) NSString *linea;
@property (nonatomic,strong) NSString *etiqueta;

@end
