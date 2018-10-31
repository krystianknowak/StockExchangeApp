import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { StockOperation } from './../_interfaces/StockOperation';


@Injectable({
  providedIn: 'root'
})
export class StockService {

  stock: StockOperation = <StockOperation>{};
  // stock: any = {};

  baseUrl = 'http://localhost:5000/api/stocks/';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
  };

constructor(private http: HttpClient) { }

buyStock(companyCode: string, ownedUnits: number) {
  this.stock.Id = 0;
  this.stock.CompanyCode = companyCode;
  this.stock.OwnedUnits = <number>ownedUnits;
  console.log(this.stock);
  this.http.post(this.baseUrl + 'buystock', this.stock , this.httpOptions);
}

// sellStock(stock: Stocks) {
//   this.http.post(this.baseUrl + 'sellstock', stock, this.httpOptions);
// }

}
