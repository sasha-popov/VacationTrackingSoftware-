import { TestBed, inject } from '@angular/core/testing';

import { VacationPoliciesService } from './vacation-policies.service';

describe('VacationPoliciesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VacationPoliciesService]
    });
  });

  it('should be created', inject([VacationPoliciesService], (service: VacationPoliciesService) => {
    expect(service).toBeTruthy();
  }));
});
