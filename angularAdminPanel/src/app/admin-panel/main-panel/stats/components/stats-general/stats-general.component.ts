import { Component, OnInit } from '@angular/core';
import { DeamonsService } from 'src/app/services/deamons.service';
import { ConfigsService } from 'src/app/services/configs.service';
import { Deamon } from 'src/app/models/deamon.model';
import { EventsService } from 'src/app/services/events.service';
import { Event } from 'src/app/models/event.model';

@Component({
  selector: 'app-stats-general',
  templateUrl: './stats-general.component.html',
  styleUrls: ['./stats-general.component.scss']
})
export class StatsGeneralComponent implements OnInit {

  public configCount: number | string = '...';
  public deamonCount: number | string = '...';
  public errorCount: number | string = '...';

  constructor(private deamonsService: DeamonsService, private configsService: ConfigsService, private eventsService: EventsService) { }

  ngOnInit(): void {
    this.configsService.findAll().subscribe(data => this.configCount = data.length );
    this.deamonsService.findAll().subscribe(data => this.deamonCount = data.length);
    // this.errorCount = this.eventsService.findAll().filter(ev => ev.type == 1 && ev.status).length + 1;
    this.eventsService.findAll().subscribe(data => this.errorCount = data.filter(ev => ev.type == 1 && ev.status).length)
  }
}
