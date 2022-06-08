import { Component, OnInit } from '@angular/core';
import { SessionsService } from 'src/app/services/sessions.service';

@Component({
  selector: 'app-top-panel',
  templateUrl: './top-panel.component.html',
  styleUrls: ['./top-panel.component.scss'],
})
export class TopPanelComponent implements OnInit {
  constructor(private service: SessionsService) {}

  ngOnInit(): void {}

  public logout(): void {
    this.service.logout();
  }
}
