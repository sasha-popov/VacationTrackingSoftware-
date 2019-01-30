import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHolidaysComponent } from './create-holidays.component';

describe('CreateHolidaysComponent', () => {
  let component: CreateHolidaysComponent;
  let fixture: ComponentFixture<CreateHolidaysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateHolidaysComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateHolidaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
