import { FpService } from './../_services/fp.service';
import { UserRegister } from './../_interfaces/UserRegister';
import { Stocks } from './../_interfaces/stocks';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  stocks: Array<Stocks> = new Array<Stocks>();
  newUser: UserRegister = <UserRegister>{};
  fpResponse: any;
  multipleOf = 1;

  constructor(private authService: AuthService, private alertify: AlertifyService, private fp: FpService) { }

  ngOnInit() {
    this.getStocksInformations();
  }

  findMultiple(companyCode: string) {
    this.multipleOf = this.fpResponse.items.find(i => i.code === companyCode).unit;
  }

  addStock(companyCode: string, unitQuantity: number) {
    if (0 >= unitQuantity) {
      return this.alertify.error('Quantity of units can not be less than 1');
    }
    const deletedStock = this.stocks.find(s => s.companycode === companyCode);
    if (deletedStock != null) {
      this.stocks.splice(this.stocks.indexOf(deletedStock), 1);
    }
    const fpItem = this.fpResponse.items.find(i => i.code === companyCode);
    if (unitQuantity % fpItem.unit !== 0) {
      return this.alertify.error(`Quantity of ${companyCode} must be multiple of ${fpItem.unit}!`);
    }
    this.stocks.push( <Stocks> {
      companycode: companyCode,
      ownedunits: unitQuantity
    });
  }

  deleteStock(companyCode: string) {
    const deletedStock = this.stocks.find(s => s.companycode === companyCode);
    this.stocks.splice(this.stocks.indexOf(deletedStock), 1);
  }

  getStocksInformations() {
    this.fp.getStocksInformations().subscribe(response => {
      this.fpResponse = response;
      console.log(response);
    }, error => {
      this.alertify.error(error);
    });
  }

  register() {
    this.newUser.username = this.model.username;
    this.newUser.password = this.model.password;
    this.newUser.firstname = this.model.firstname;
    this.newUser.lastname = this.model.lastname;
    this.newUser.availablemoney = this.model.availablemoney;
    this.newUser.stocks = this.stocks;
    console.log(this.newUser);
    this.authService.register(this.newUser).subscribe(() => {
      this.alertify.success('registration succesful');
    }, error => {
      this.alertify.error(error);
    });
  }
}
