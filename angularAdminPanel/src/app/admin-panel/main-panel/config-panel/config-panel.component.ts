import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Config } from 'src/app/models/config.model';
import { Destination } from 'src/app/models/destination.model';
import { ConfigsService } from 'src/app/services/configs.service';
import { DestinationsServices } from 'src/app/services/destinations.service';

@Component({
  selector: 'app-config-panel',
  templateUrl: './config-panel.component.html',
  styleUrls: ['./config-panel.component.scss'],
})
export class ConfigPanelComponent implements OnInit {
  public Destinations: Destination[];

  addItem(dialogue: string) {
    this.activeDia = dialogue;
  }

  addDest(dest: Destination) {

    console.log("💜", dest)
    let dst = this.Destinations.find(dst => dst.id == dest.id)
    if(dst) {
      const ix = this.Destinations.indexOf(dst)
      this.Destinations.splice(ix, 1, dest)
    }
    else this.Destinations.push(dest);
  }

  public selected: Config;
  public activeDia: string = '';

  constructor(
    private route: ActivatedRoute,
    private configsService: ConfigsService,
    private destinationsService: DestinationsServices
  ) {}

  ngOnInit(): void {
    let id = +this.route.snapshot.params['id'];
    this.configsService.findById(id).subscribe(data => this.selected = data);

    this.destinationsService
      .findById(id)
      .subscribe((data) => (this.Destinations = data));
  }
}
