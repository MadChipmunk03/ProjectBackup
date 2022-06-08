import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaConfigsFilterComponent } from './dia-configs-filter.component';

describe('DiaConfigsFilterComponent', () => {
  let component: DiaConfigsFilterComponent;
  let fixture: ComponentFixture<DiaConfigsFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaConfigsFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaConfigsFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
