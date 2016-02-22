//
//  TarjetaTableViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 03/02/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"
@interface TarjetaTableViewController : UITableViewController
@property (nonatomic,strong)  NSMutableArray *tarjeta;
@property (nonatomic,strong)  NSMutableArray *subtarjeta;
@property (nonatomic,strong) LoadingView *load;

@end
