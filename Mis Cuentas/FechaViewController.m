//
//  FechaViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 03/02/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "FechaViewController.h"
#import "CameraViewController.h"
@interface FechaViewController ()

@end

@implementation FechaViewController
@synthesize diario=_diario,linea=_linea,date=_date,etiqueta=_etiqueta;
-(void)myAction:(id)sender
{
    fechaSeleccionada=[_date.date.description substringToIndex:10];
    
}
- (void)viewDidLoad {
    [super viewDidLoad];
    [self.date addTarget:self
                          action:@selector(myAction:)
                forControlEvents:UIControlEventValueChanged];
    fechaSeleccionada=[_date.date.description substringToIndex:10];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    
    if ([[segue identifier] isEqualToString:@"vesASiCamara"])
    {
        CameraViewController *per = (CameraViewController *)[segue destinationViewController];
        [per setLinea:_linea];
        [per setDiario:_diario];
        [per setDeboPonerCamara:NO];
        [per setEtiqueta:_etiqueta];
        [per setFechaSeleccionada:fechaSeleccionada];
    }
}


@end
