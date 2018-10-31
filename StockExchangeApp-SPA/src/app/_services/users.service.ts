import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_interfaces/userData';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:5000/api/users';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
  };

  getStockExchnage() {
    return this.http.get<User>(this.baseUrl + '/stockexchnage', this.httpOptions);
  }

  getUser() {
    return this.http.get<User>(this.baseUrl, this.httpOptions);
  }

}
