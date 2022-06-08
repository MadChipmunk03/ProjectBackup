import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaDestinationComponent } from './dia-destination.component';

describe('DiaDestinationComponent', () => {
  let component: DiaDestinationComponent;
  let fixture: ComponentFixture<DiaDestinationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaDestinationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaDestinationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
