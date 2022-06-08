import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewBackupComponent } from './new-backup.component';

describe('NewBackupComponent', () => {
  let component: NewBackupComponent;
  let fixture: ComponentFixture<NewBackupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NewBackupComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewBackupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
