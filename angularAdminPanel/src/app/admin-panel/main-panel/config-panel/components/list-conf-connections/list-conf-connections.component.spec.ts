import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListConfConnectionsComponent } from './list-conf-connections.component';

describe('ListConfConnectionsComponent', () => {
  let component: ListConfConnectionsComponent;
  let fixture: ComponentFixture<ListConfConnectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListConfConnectionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListConfConnectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
