/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FpService } from './fp.service';

describe('Service: Fp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FpService]
    });
  });

  it('should ...', inject([FpService], (service: FpService) => {
    expect(service).toBeTruthy();
  }));
});
