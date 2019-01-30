import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVacationRequestComponent } from './create-vacation-request.component';

describe('CreateVacationRequestComponent', () => {
  let component: CreateVacationRequestComponent;
  let fixture: ComponentFixture<CreateVacationRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateVacationRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateVacationRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
