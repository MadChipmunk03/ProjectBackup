import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-overlay',
  templateUrl: './overlay.component.html',
  styleUrls: ['./overlay.component.scss']
})
export class OverlayComponent implements OnInit {

  public ok = function () {
    console.log('deamon');
  }
  
  constructor() { }

  ngOnInit(): void {
  }

}
