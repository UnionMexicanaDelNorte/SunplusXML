//
//  SolicitudTableViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 28/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"

@interface SolicitudTableViewController : UITableViewController
{
    NSString *diarioLineaS;
}
@property (nonatomic,strong)  NSMutableArray *presupuesto;
@property (nonatomic,strong)  NSMutableArray *detailPresupuesto;
@property (nonatomic,strong) LoadingView *load;
@property (nonatomic,strong)  NSMutableArray *diarioLinea;
@end
