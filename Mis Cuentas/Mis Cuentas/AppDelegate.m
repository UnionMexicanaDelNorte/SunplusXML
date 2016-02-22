//
//  AppDelegate.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 05/10/15.
//  Copyright © 2015 UMN. All rights reserved.
//

#import "AppDelegate.h"
#import "Reachability.h"

#import "DetailViewController.h"

@interface AppDelegate () <UISplitViewControllerDelegate>
@property (nonatomic) Reachability *internetReachability;

@end

@implementation AppDelegate
@synthesize hasInternet=_hasInternet;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
   // UISplitViewController *splitViewController = (UISplitViewController *)self.window.rootViewController;
   // UINavigationController *navigationController = [splitViewController.viewControllers lastObject];
    //navigationController.topViewController.navigationItem.leftBarButtonItem = splitViewController.displayModeButtonItem;
    //splitViewController.delegate = self;
    
    self.internetReachability = [Reachability reachabilityForInternetConnection];
    [self.internetReachability startNotifier];
    [self updateInterfaceWithReachability:self.internetReachability];
    
    [[NSUserDefaults standardUserDefaults] setObject:@"ERALOFE01" forKey:@"WHO"];
    [[NSUserDefaults standardUserDefaults] setObject:@"sunplus.redirectme.net:90" forKey:@"URLyPUERTO"];
    
    NSDate *hoy = [NSDate date];
    
    
    NSDateComponents *components = [[NSCalendar currentCalendar] components:NSCalendarUnitDay | NSCalendarUnitMonth | NSCalendarUnitYear fromDate:hoy];
    
    
    int month = (int)[components month];
    int year = (int)[components year];
    NSString *M;
    if(month<10)
    {
        M=[NSString stringWithFormat:@"00%d",month];
    }
    else
    {
        M=[NSString stringWithFormat:@"0%d",month];
    }
    NSString * aver =[NSString stringWithFormat:@"%d%@",year,M] ;
    [[NSUserDefaults standardUserDefaults] setObject:aver forKey:@"PERIOD"];
    
     //app push
    self.firstRun=NO;
    if(![[NSUserDefaults standardUserDefaults] boolForKey:@"AlreadyRan"] ) {
        [[NSUserDefaults standardUserDefaults] setBool:TRUE forKey:@"AlreadyRan"];
        self.firstRun = YES;
    }
    
    [[NSUserDefaults standardUserDefaults] synchronize];
    
   
    
    return YES;
}
- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error {
    NSLog(@"Did Fail to Register for Remote Notifications");
    NSLog(@"%@, %@", error, error.localizedDescription);
    
}
- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    NSLog(@"Did Register for Remote Notifications with Device Token (%@)", deviceToken);
    
    dispatch_async( dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
        NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
        
        NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
        
        NSString *urlString = [NSString stringWithFormat:@"%@&accion=4&argumento1=%@&argumento2=%@&argumento3=1",[defaults valueForKey:@"url"],[defaults valueForKey:@"ER"],deviceToken];
        NSCharacterSet *set = [NSCharacterSet URLQueryAllowedCharacterSet];
        NSString* encodedUrl = [urlString stringByAddingPercentEncodingWithAllowedCharacters:
                                set];
        NSURL * url = [NSURL URLWithString:encodedUrl];
        NSMutableURLRequest * urlRequest = [NSMutableURLRequest requestWithURL:url];
        [urlRequest setHTTPMethod:@"GET"];//GET
        
        
        
        
        NSURLSessionDataTask * dataTask =[defaultSession dataTaskWithRequest:urlRequest                                                               completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
            if(error == nil)
            {
                NSError* error;
                NSDictionary* json = [NSJSONSerialization
                                      JSONObjectWithData:data
                                      options:kNilOptions
                                      error:&error];
                int success = [[json objectForKey:@"success"] intValue];
                if(success==1)
                {
                    //me regreso
                    
                    
                    
                }
                else
                {
                    
                }
                
                
            }
         }];
        
        [dataTask resume];
        
        
    });
}
/*!
 * Called by Reachability whenever status changes.
 */
- (void) reachabilityChanged:(NSNotification *)note
{
    Reachability* curReach = [note object];
    NSParameterAssert([curReach isKindOfClass:[Reachability class]]);
    [self updateInterfaceWithReachability:curReach];
}


- (void)updateInterfaceWithReachability:(Reachability *)reachability
{
    if (reachability == self.internetReachability)
    {
        [self configureTextField:reachability];
    }
    
}


- (void)configureTextField:(Reachability *)reachability
{
    NetworkStatus netStatus = [reachability currentReachabilityStatus];
    BOOL connectionRequired = [reachability connectionRequired];
    //   NSString* statusString = @"";
    
    switch (netStatus)
    {
        case NotReachable:        {
            _hasInternet=NO;
            break;
        }
            
        case ReachableViaWWAN:        {
            _hasInternet=YES;
            // statusString = NSLocalizedString(@"Reachable WWAN", @"");
            // imageView.image = [UIImage imageNamed:@"WWAN5.png"];
            break;
        }
        case ReachableViaWiFi:        {
            _hasInternet=YES;
            // statusString= NSLocalizedString(@"Reachable WiFi", @"");
            //    imageView.image = [UIImage imageNamed:@"Airport.png"];
            break;
        }
    }
    
    if (connectionRequired)
    {
        // NSString *connectionRequiredFormatString = NSLocalizedString(@"%@, Connection Required", @"Concatenation of status string with connection requirement");
        // statusString= [NSString stringWithFormat:connectionRequiredFormatString, statusString];
    }
    //   textField.text= statusString;
}




- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

#pragma mark - Split view

- (BOOL)splitViewController:(UISplitViewController *)splitViewController collapseSecondaryViewController:(UIViewController *)secondaryViewController ontoPrimaryViewController:(UIViewController *)primaryViewController {
    if ([secondaryViewController isKindOfClass:[UINavigationController class]] && [[(UINavigationController *)secondaryViewController topViewController] isKindOfClass:[DetailViewController class]] && ([(DetailViewController *)[(UINavigationController *)secondaryViewController topViewController] detailItem] == nil)) {
        // Return YES to indicate that we have handled the collapse by doing nothing; the secondary controller will be discarded.
        return YES;
    } else {
        return NO;
    }
}

@end
