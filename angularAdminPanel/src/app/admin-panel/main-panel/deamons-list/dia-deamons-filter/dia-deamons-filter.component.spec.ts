import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaDeamonsFilterComponent } from './dia-deamons-filter.component';

describe('DiaDeamonsFilterComponent', () => {
  let component: DiaDeamonsFilterComponent;
  let fixture: ComponentFixture<DiaDeamonsFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaDeamonsFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaDeamonsFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
