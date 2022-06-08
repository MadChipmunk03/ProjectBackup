import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeamonPanelComponent } from './deamon-panel.component';

describe('DeamonPanelComponent', () => {
  let component: DeamonPanelComponent;
  let fixture: ComponentFixture<DeamonPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeamonPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeamonPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
