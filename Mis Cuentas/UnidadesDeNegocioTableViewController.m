//
//  UnidadesDeNegocioTableViewController.m
//  Mis Cuentas
//
//  Created by Fernando Alonso on 26/01/16.
//  Copyright © 2016 UMN. All rights reserved.
//

#import "UnidadesDeNegocioTableViewController.h"

@interface UnidadesDeNegocioTableViewController ()

@end

@implementation UnidadesDeNegocioTableViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    NSUserDefaults *defaults =[NSUserDefaults standardUserDefaults];
    celdas = [defaults objectForKey:@"unidades"];
    selecccionado = [defaults objectForKey:@"unidadSeleccionada"];
    
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark - Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return celdas.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"unidadesDeNegocioCell" forIndexPath:indexPath];
    NSDictionary *dic=[celdas objectAtIndex:indexPath.row];
    cell.textLabel.text=[dic objectForKey:@"nombreAmigable"];
    cell.detailTextLabel.text=[dic objectForKey:@"nombre"];
    if([selecccionado isEqualToString:[dic objectForKey:@"nombre"]])
    {
        [cell setAccessoryType:UITableViewCellAccessoryCheckmark];
    }
    return cell;
}
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSDictionary *dic=[celdas objectAtIndex:indexPath.row];
    NSString *currentCell =[dic objectForKey:@"nombre"];
    [[NSUserDefaults standardUserDefaults] setObject:currentCell forKey:@"unidadSeleccionada"];
    [[NSUserDefaults standardUserDefaults]synchronize];
    [self dismissViewControllerAnimated:YES completion:nil];
}
/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
