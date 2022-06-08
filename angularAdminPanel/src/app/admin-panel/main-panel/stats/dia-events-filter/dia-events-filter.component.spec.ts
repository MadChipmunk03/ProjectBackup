import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaEventsFilterComponent } from './dia-events-filter.component';

describe('DiaEventsFilterComponent', () => {
  let component: DiaEventsFilterComponent;
  let fixture: ComponentFixture<DiaEventsFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaEventsFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaEventsFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
