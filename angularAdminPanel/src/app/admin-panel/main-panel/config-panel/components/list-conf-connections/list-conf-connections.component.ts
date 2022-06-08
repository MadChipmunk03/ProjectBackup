import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigsService } from 'src/app/services/configs.service';
import { Config } from 'src/app/models/config.model';
import { Connection } from 'src/app/models/connection.model';
import { ConnectionsServices } from 'src/app/services/connections.service';
import { DeamonsService } from 'src/app/services/deamons.service';
import { Deamon } from 'src/app/models/deamon.model';

@Component({
  selector: 'app-list-conf-connections',
  templateUrl: './list-conf-connections.component.html',
  styleUrls: ['./list-conf-connections.component.scss'],
})
export class ListConfConnectionsComponent implements OnInit {
  public selected: Config;
  public connections: Connection[] = [];
  public deamons: Deamon[] = [];

  public connectionsLoaded = false;
  public deamonsLoaded = false;

  constructor(
    private route: ActivatedRoute,
    private configsService: ConfigsService,
    private connectionService: ConnectionsServices,
    private deamonsService: DeamonsService
  ) {}

  public flipConnection(id: number): void {
    const conn: Connection | undefined = this.connections.find(
      (con) => con.id === id
    );
    if (conn != undefined) {
      conn.connected = !conn.connected;
      this.connectionService.put(conn);
    }
  }

  public findAlias(id: number): string | undefined {
    return this.deamons.find((conf) => conf.id === id)?.alias;
  }

  ngOnInit(): void {
    let id = +this.route.snapshot.params['id'];
    this.configsService
      .findById(id)
      .subscribe((data) => (this.selected = data));
    this.configsService
      .findById(id)
      .subscribe((data) => (this.selected = data));
    this.connectionService.findConfsCon(id).subscribe((data) => {
      this.connections = data;
      console.log(this.connections);
      this.connectionsLoaded = true;
    });
    this.deamonsService.findAll().subscribe((data) => {
      this.deamons = data;
      this.deamonsLoaded = true;
    });
  }
}
