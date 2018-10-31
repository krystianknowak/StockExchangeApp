import { element } from 'protractor';
import { FpService } from './../_services/fp.service';
import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, Input } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { User, Stock } from '../_interfaces/userData';
import { FpResponse } from '../_interfaces/FpResponse';
import { StockService } from '../_services/stock.service';

@Component({
  selector: 'app-buy-stock',
  templateUrl: './buy-stock.component.html',
  styleUrls: ['./buy-stock.component.css']
})
export class BuyStockComponent implements OnInit {

  fpResponse: FpResponse;
  user: User;
  stocks: Stock[] = [];

  constructor(private usersService: UsersService,
    private alertify: AlertifyService,
    private fp: FpService,
    private stockService: StockService) { }

  ngOnInit() {
    this.getStockExchange();
  }

  buyStock(companyCode: string) {
    this.alertify.prompt(companyCode, 'buy');
  }

  getStockExchange() {
    this.usersService.getStockExchnage().subscribe(stockExchange => {
      this.stocks = stockExchange.stocks;
      this.getStocksInformations(this.user);
    }, error => {
      this.alertify.error(error);
    });
  }

  getStocksInformations(item: User) {
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
