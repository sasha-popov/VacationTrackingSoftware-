import { TestBed } from '@angular/core/testing';

import { HeaderService } from '../../Services/HeaderService/header-service.service';

describe('HeaderServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HeaderService = TestBed.get(HeaderService);
    expect(service).toBeTruthy();
  });
});