import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleTeamsComponent } from './schedule-teams.component';

describe('ScheduleTeamsComponent', () => {
  let component: ScheduleTeamsComponent;
  let fixture: ComponentFixture<ScheduleTeamsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduleTeamsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleTeamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
