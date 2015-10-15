//
//  DetailViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface DetailViewController : UIViewController

@property (strong, nonatomic) id detailItem;
@property (weak, nonatomic) IBOutlet UILabel *detailDescriptionLabel;

@end

