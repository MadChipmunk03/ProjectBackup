import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  
  public activeDialogue: string = '';

  public changeDialogue(dialogue: string): void{
    this.activeDialogue = dialogue;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
