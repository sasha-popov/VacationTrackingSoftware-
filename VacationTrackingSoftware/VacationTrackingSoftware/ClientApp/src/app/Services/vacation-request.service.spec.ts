import { TestBed, inject } from '@angular/core/testing';

import { VacationRequestService } from './vacation-request.service';

describe('VacationRequestService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VacationRequestService]
    });
  });

  it('should be created', inject([VacationRequestService], (service: VacationRequestService) => {
    expect(service).toBeTruthy();
  }));
});
