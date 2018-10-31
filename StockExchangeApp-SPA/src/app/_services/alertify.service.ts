import { StockService } from './stock.service';
import { Stocks } from './../_interfaces/stocks';
import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor(private stockService: StockService) {}

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, function(e) {
      if (e) {
        okCallback();
      } else {
      }
    });
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }

  prompt(companyCode: string, action: string) {
    alertify.prompt('Specify quantity of units', '1',
      (evt, value) => {
        if (isNaN(value)) {
          return alertify.error('Sending value must be a number');
        }
        this.stockService.buyStock(companyCode, value);
        // doStock(companyCode, value);
        // alertify.success(value);
      },
      (evt, value) => {
      }
    );
  }
}
