//
//  MasterViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"
@class DetailViewController;

@interface MasterViewController : UITableViewController
{
    LoadingView *_load;
}
@property (strong, nonatomic) DetailViewController *detailViewController;
@property (nonatomic,strong)  NSMutableArray *periodos;
-(IBAction)menuPeriodos:(id)sender;
@property (strong,nonatomic) IBOutlet UIBarButtonItem *barButton;
@end

