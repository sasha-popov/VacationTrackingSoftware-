import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVacationPolicyComponent } from './create-vacation-policy.component';

describe('CreateVacationPolicyComponent', () => {
  let component: CreateVacationPolicyComponent;
  let fixture: ComponentFixture<CreateVacationPolicyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateVacationPolicyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateVacationPolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
