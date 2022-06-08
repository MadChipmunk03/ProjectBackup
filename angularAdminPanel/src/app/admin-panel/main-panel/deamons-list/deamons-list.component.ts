import { Component, OnInit } from '@angular/core';
import { Deamon } from '../../../models/deamon.model';
import { DeamonsService } from '../../../services/deamons.service';

@Component({
  selector: 'app-deamons-list',
  templateUrl: './deamons-list.component.html',
  styleUrls: ['./deamons-list.component.scss'],
})
export class DeamonsListComponent implements OnInit {
  public activeDialogue: string = '';

  public deamons: Deamon[] = [];

  public loaded: boolean = false;

  public statStr(stat: number): string {
    return this.service.statusToStr(stat);
  }

  public changeDialogue(dialogue: string): void {
    this.activeDialogue = dialogue;
  }

  constructor(private service: DeamonsService) {}

  ngOnInit(): void {
    this.service.findAll().subscribe((data) => {
      this.deamons = data;
      this.loaded = true;
    });
  }
}
