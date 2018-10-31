import { User } from './../_interfaces/userData';
import { Component, OnInit } from '@angular/core';
import { FpService } from '../_services/fp.service';
import { AlertifyService } from '../_services/alertify.service';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-sell-stock',
  templateUrl: './sell-stock.component.html',
  styleUrls: ['./sell-stock.component.css']
})

export class SellStockComponent implements OnInit {

  fpResponse: any;

  stocks: any = [];
  money: number;

  constructor(private fp: FpService, private alertify: AlertifyService, private usersService: UsersService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.usersService.getUser().subscribe(user => {
      this.money = user.availableMoney;
      this.stocks = user.stocks;
      this.getStocksInformations();
    }, error => {
      this.alertify.error(error);
    });
  }

  getStocksInformations() {
    this.fp.getStocksInformations().subscribe(response => {
      this.fpResponse = response;
      this.priceToStocks();
    }, error => {
      this.alertify.error(error);
    });
  }

  priceToStocks() {
    // tslint:disable-next-line:no-shadowed-variable
    this.stocks.forEach(element => {
      const itemFromResponse = this.fpResponse.items.find(i => i.code === element.companyCode);
      element.value = itemFromResponse.price;
    });
  }
}
