//
//  LoginViewController.h
//  Mis Cuentas
//
//  Created by Fernando Alonso on 19/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface LoginViewController : UIViewController <UITextFieldDelegate>
@property (strong, nonatomic) IBOutlet UITextField *correo;
@property (strong, nonatomic) IBOutlet UITextField *pass;
@property (strong, nonatomic) IBOutlet UISwitch *recordarme;
-(IBAction)entrar:(id)sender;
-(IBAction)entrarComoInvitado:(id)sender;
@end
