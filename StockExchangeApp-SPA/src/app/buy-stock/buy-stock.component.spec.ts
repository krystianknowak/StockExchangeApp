/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BuyStockComponent } from './buy-stock.component';

describe('BuyStockComponent', () => {
  let component: BuyStockComponent;
  let fixture: ComponentFixture<BuyStockComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuyStockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuyStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
