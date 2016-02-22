//
//  BeneficiarioTableViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 28/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "LoadingView.h"
@interface BeneficiarioTableViewController : UITableViewController <UISearchBarDelegate, UISearchControllerDelegate, UISearchResultsUpdating>
@property (nonatomic,strong) NSString *diario;
@property (nonatomic,strong) NSString *linea;
@property (nonatomic,strong)  NSMutableArray *beneficiarios;
@property (nonatomic,strong)  NSArray *searchResults;
@property (nonatomic,strong) LoadingView *load;
@property (nonatomic, strong) UISearchController *searchController;
@property BOOL searchControllerWasActive;
@property BOOL searchControllerSearchFieldWasFirstResponder;

@end
