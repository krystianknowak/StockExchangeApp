import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AppRoutes } from './routes';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { PanelComponent } from './panel/panel.component';
import { BuyStockComponent } from './buy-stock/buy-stock.component';
import { SellStockComponent } from './sell-stock/sell-stock.component';
import { HomeComponent } from './home/home.component';
import { FpService } from './_services/fp.service';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      PanelComponent,
      BuyStockComponent,
      SellStockComponent,
      HomeComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(AppRoutes),
      FormsModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      FpService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
