import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateVacationPolicyComponent } from './update-vacation-policy.component';

describe('UpdateVacationPolicyComponent', () => {
  let component: UpdateVacationPolicyComponent;
  let fixture: ComponentFixture<UpdateVacationPolicyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateVacationPolicyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateVacationPolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
