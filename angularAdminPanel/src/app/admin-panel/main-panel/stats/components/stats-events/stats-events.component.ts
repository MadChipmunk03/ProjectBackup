import { Component, OnInit } from '@angular/core';

import { Event } from '../../../../../models/event.model';
import { Connection } from '../../../../../models/connection.model';
import { Config } from '../../../../../models/config.model';
import { Deamon } from '../../../../../models/deamon.model';

import { EventsService } from '../../../../../services/events.service';
import { ConnectionsServices } from 'src/app/services/connections.service';
import { DeamonsService } from 'src/app/services/deamons.service';
import { ConfigsService } from 'src/app/services/configs.service';

@Component({
  selector: 'app-stats-events',
  templateUrl: './stats-events.component.html',
  styleUrls: ['./stats-events.component.scss'],
})
export class StatsEventsComponent implements OnInit {
  events: Event[] = [];
  connections: Connection[] = [];
  deamons: Deamon[] = [];
  configs: Config[] = [];

  public allLoaded: number = 0;

  public typeStr(type: number): string {
    return this.service.TypeToStr(type);
  }

  public getDeamonAlias(connectionId: number): string | null {
    const connection = this.connectionsService.findById(connectionId);
    if (connection == null) return '';

    return this.deamons.find((deam) => deam.id == connectionId)?.alias || '';
  }

  public getConfigAlias(connectionId: number): string | null {
    const connection = this.connections.find(
      (con) => con.configId == connectionId
    );
    if (connection == null) return '';
    return this.configs.find((conf) => conf.id == connectionId)?.alias || '';
  }

  constructor(
    private service: EventsService,
    private connectionsService: ConnectionsServices,
    private deamonsService: DeamonsService,
    private configsService: ConfigsService
  ) {}

  ngOnInit(): void {
    this.service.findAll().subscribe((data) => {
      this.events = data.filter((ev) => ev.status);
      this.allLoaded++;
    });
    this.connectionsService.findAll().subscribe((data) => {
      this.connections = data;
      this.allLoaded++;
    });
    this.configsService.findAll().subscribe((data) => {
      this.configs = data;
      this.allLoaded++;
    });
    this.deamonsService.findAll().subscribe((data) => {
      this.deamons = data;
      this.allLoaded++;
    });
  }
}
