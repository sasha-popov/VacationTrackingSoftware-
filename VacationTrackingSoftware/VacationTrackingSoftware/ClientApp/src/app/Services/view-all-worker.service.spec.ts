import { TestBed } from '@angular/core/testing';

import { allWorkerService } from './allWorkerService';

describe('ViewAllWorkerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: allWorkerService = TestBed.get(allWorkerService);
    expect(service).toBeTruthy();
  });
});
