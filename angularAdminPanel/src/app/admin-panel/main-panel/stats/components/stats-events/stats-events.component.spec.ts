import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatsEventsComponent } from './stats-events.component';

describe('StatsEventsComponent', () => {
  let component: StatsEventsComponent;
  let fixture: ComponentFixture<StatsEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatsEventsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatsEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
