import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FpService {

constructor(private http: HttpClient) { }

  fpUrl = 'http://localhost:5000/api/fp';

  getStocksInformations() {
    const myHeaders = new HttpHeaders();
    myHeaders.append('Content-Type', 'application/json');
    return this.http.get(this.fpUrl, {headers: myHeaders });
  }

}
