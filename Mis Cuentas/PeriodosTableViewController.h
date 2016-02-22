//
//  PeriodosTableViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 19/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"
@interface PeriodosTableViewController : UITableViewController
{
    LoadingView *load;
}
@property (nonatomic,strong)  NSArray *periodos;
@end
