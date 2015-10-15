//
//  MasterViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>

@class DetailViewController;

@interface MasterViewController : UITableViewController

@property (strong, nonatomic) DetailViewController *detailViewController;

@end

