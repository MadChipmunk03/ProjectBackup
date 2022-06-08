import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeamonsListComponent } from './deamons-list.component';

describe('DeamonsListComponent', () => {
  let component: DeamonsListComponent;
  let fixture: ComponentFixture<DeamonsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeamonsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeamonsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
