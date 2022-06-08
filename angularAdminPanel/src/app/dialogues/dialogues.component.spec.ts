import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialoguesComponent } from './dialogues.component';

describe('DialoguesComponent', () => {
  let component: DialoguesComponent;
  let fixture: ComponentFixture<DialoguesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialoguesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialoguesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
