import { Component, OnInit } from '@angular/core';
import { Config } from 'src/app/models/config.model';
import { ConfigsService } from 'src/app/services/configs.service';

@Component({
  selector: 'app-configs-list',
  templateUrl: './configs-list.component.html',
  styleUrls: ['./configs-list.component.scss'],
})
export class ConfigsListComponent implements OnInit {
  public configs: Config[] = [];

  public loaded: boolean = false;

  public activeDialogue: string = '';

  constructor(private service: ConfigsService) {}

  public changeDialogue(dialogue: string): void {
    this.activeDialogue = dialogue;
  }

  ngOnInit(): void {
    this.service.findAll().subscribe((data) => {
      this.configs = data;
      this.loaded = true;
    });
  }
}
