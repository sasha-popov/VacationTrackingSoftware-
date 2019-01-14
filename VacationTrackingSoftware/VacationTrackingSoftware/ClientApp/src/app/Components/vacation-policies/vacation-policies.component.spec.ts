import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VacationPoliciesComponent } from './vacation-policies.component';

describe('VacationPoliciesComponent', () => {
  let component: VacationPoliciesComponent;
  let fixture: ComponentFixture<VacationPoliciesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VacationPoliciesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VacationPoliciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
