import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatsGeneralComponent } from './stats-general.component';

describe('StatsGeneralComponent', () => {
  let component: StatsGeneralComponent;
  let fixture: ComponentFixture<StatsGeneralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatsGeneralComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatsGeneralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
