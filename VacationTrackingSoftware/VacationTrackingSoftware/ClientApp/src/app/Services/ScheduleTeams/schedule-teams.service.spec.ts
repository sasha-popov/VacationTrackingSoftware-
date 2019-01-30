import { TestBed } from '@angular/core/testing';

import { ScheduleTeamsService } from './schedule-teams.service';

describe('ScheduleTeamsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ScheduleTeamsService = TestBed.get(ScheduleTeamsService);
    expect(service).toBeTruthy();
  });
});
