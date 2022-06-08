import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Connection } from 'src/app/models/connection.model';
import { ConnectionsServices } from 'src/app/services/connections.service';
import { DeamonsService } from 'src/app/services/deamons.service';
import { Deamon } from 'src/app/models/deamon.model';
import { EventsService } from 'src/app/services/events.service';
import { Event } from 'src/app/models/event.model';
import { ConfigsService } from 'src/app/services/configs.service';
import { Config } from 'src/app/models/config.model';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-deamon-panel',
  templateUrl: './deamon-panel.component.html',
  styleUrls: ['./deamon-panel.component.scss'],
})
export class DeamonPanelComponent implements OnInit {
  public form: FormGroup;
  private newDeam: Deamon;

  public selected: Deamon;
  public events: Event[] = [];
  public connections: Connection[] = [];
  public configs: Config[] = [];

  public deamonLoaded: boolean = false;
  public connectionsLoaded: boolean = false;
  public eventsLoaded: boolean = false;
  public configsLoaded: boolean = false;

  public statStr(stat: number): string {
    // return this.deamonsService.statusToStr(stat);
    return this.deamonsService.statusToStr(stat % 2);
  }
  public disabeledStr(stat: number): string {
    return stat >= 2 ? '⏸Disabeled' : '🆗Enabeled';
  }

  public findAlias(id: number): string | undefined {
    return this.configs.find((conf) => conf.id === id)?.alias;
  }
  public findAliasForConnection(id: number): string | undefined {
    return this.findAlias(
      this.connections.find((con) => con.id === id)?.configId || 0
    );
  }

  public flipConnection(id: number): void {
    const conn: Connection | undefined = this.connections.find(
      (con) => con.id === id
    );
    if (conn != undefined) {
      conn.connected = !conn.connected;
      this.connectionService.put(conn);
    }
  }

  public changeStatus(): void {
    this.selected.status = (this.selected.status + 2) % 4;
    console.log(this.selected.status);
  }

  public saveChanges(): void {
    const vals = this.form.value;
    console.log(vals);

    this.newDeam.alias = vals.alias;
    this.newDeam.status = this.selected.status;
    this.newDeam.id = this.selected.id;
    console.log(this.newDeam);

    this.deamonsService.put(this.newDeam);
  }

  public deleteDeam(): void {
    this.deamonsService.delete(this.selected.id);
  }

  constructor(
    private route: ActivatedRoute,
    private deamonsService: DeamonsService,
    private fb: FormBuilder,
    private eventsService: EventsService,
    private connectionService: ConnectionsServices,
    private configsService: ConfigsService
  ) {}

  ngOnInit(): void {
    this.newDeam = new Deamon();

    let id = +this.route.snapshot.params['id'];
    this.deamonsService.findById(id).subscribe((data) => {
      this.selected = data;
      this.form = this.createForm();
      this.deamonLoaded = true;
    });
    this.connectionService.findDeamsCon(id).subscribe((data) => {
      this.connections = data;
      this.connectionsLoaded = true;
      data.forEach((con) => {
        con.events.forEach((ev) => this.events.push(ev));
      });
      this.eventsLoaded = true;
    });
    this.configsService.findAll().subscribe((data) => {
      this.configs = data;
      this.configsLoaded = true;
    });

    this.selected?.status || 0;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      alias: this.selected.alias,
      status: this.selected.status,
    });
  }
}
