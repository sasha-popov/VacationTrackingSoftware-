import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllWorkersComponent } from './view-all-workers.component';

describe('ViewAllWorkersComponent', () => {
  let component: ViewAllWorkersComponent;
  let fixture: ComponentFixture<ViewAllWorkersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewAllWorkersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAllWorkersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
