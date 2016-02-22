//
//  PresupuestoTableViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 12/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"
@interface PresupuestoTableViewController : UITableViewController
{
    NSString *diarioLineaS;
    NSString *etiquetaGlobal;
}
@property (nonatomic,strong)  NSMutableArray *presupuesto;
@property (nonatomic,strong)  NSMutableArray *detailPresupuesto;
@property (nonatomic,strong)  NSMutableArray *diarioLinea;
@property (nonatomic,strong) LoadingView *load;
@property (nonatomic,strong) IBOutlet UISwitch *switchConFactura;
@end
