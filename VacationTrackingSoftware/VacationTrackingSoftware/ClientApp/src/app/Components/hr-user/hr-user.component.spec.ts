import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HrUserComponent } from './hr-user.component';

describe('HrUserComponent', () => {
  let component: HrUserComponent;
  let fixture: ComponentFixture<HrUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HrUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HrUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
