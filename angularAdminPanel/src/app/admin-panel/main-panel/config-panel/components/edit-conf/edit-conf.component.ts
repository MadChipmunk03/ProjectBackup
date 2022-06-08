import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Config } from 'src/app/models/config.model';
import { ConfigsService } from 'src/app/services/configs.service';
import { Destination } from 'src/app/models/destination.model';
import { DestinationsServices } from 'src/app/services/destinations.service';
import { timePeriodToCron } from 'src/app/helper';
import { Source } from 'src/app/models/source.model';
import { SourcesService } from 'src/app/services/sources.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-conf',
  templateUrl: './edit-conf.component.html',
  styleUrls: ['./edit-conf.component.scss'],
})
export class EditConfComponent implements OnInit {
  @Output() activeDia = new EventEmitter<string>();
  @Input() Destinations: Destination[];

  public saveBtnText: String = 'Save changes'
  public form: FormGroup;
  public formLoaded: boolean = false;

  public selected: Config;
  public sources: Source[] = [];

  constructor(
    // private router: Route,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private http: HttpClient,
    private configsService: ConfigsService,
    private destinationsSertice: DestinationsServices,
    private sourcesService: SourcesService
  ) {}

  public editDest(destNum: number): void {
    this.activeDia.emit(`addDest;${destNum}`);
  }

  getState(): string {
    return this.selected.active ? '🆗 Enabeled' : '⏸ Disabeled';
  }
  switchState(): void {
    this.selected.active = !this.selected.active;
  }

  ngOnInit(): void {
    let id = +this.route.snapshot.params['id'];

    this.configsService.findById(id).subscribe((data) => {
      this.selected = data;
      this.form = this.createForm();
      this.formLoaded = true;
    });

    this.sourcesService.findById(id).subscribe((data) => (this.sources = data));
  }

  public async btnRemoveSource(id: number): Promise<void> {
    await this.sourcesService.delete(id);
    this.sources = this.sources.filter((src) => src.id != id);
  }

  public enterPath: string;
  public btnAddSource(): void {
    const source: Source = new Source();
    const path = this.form.value.sourcePath;
    source.configId = this.selected.id;
    source.sourcePath = path;

    this.form.patchValue({
      sourcePath: '',
    });

    this.sourcesService
      .post(source)
      .subscribe((data) => this.sources.push(data));
  }

  public btnRemoveDest(id: number) {
    console.log("we are here", id)
    // const ix = this.Destinations.find(dest => dest.id === id)?.id || 0;
    // console.log(ix, this.Destinations)
    this.Destinations = this.Destinations.filter((src) => src.id != id);
    this.destinationsSertice.delete(id);
  }

  public btnAddDest(): void {
    this.activeDia.emit('addDest');
  }

  public saveTypeStr(saveType: number): string {
    return this.destinationsSertice.saveTypeToStr(saveType);
  }

  public async saveChanges(): Promise<void> {
    const vals = this.form.value;
    console.log(vals);

    let timePeriod = '';
    if (vals.buDaysMo) timePeriod += 'Mon,';
    if (vals.buDaysTu) timePeriod += 'Tue,';
    if (vals.buDaysWe) timePeriod += 'Wed,';
    if (vals.buDaysTh) timePeriod += 'Thu,';
    if (vals.buDaysFr) timePeriod += 'Fri,';
    if (vals.buDaysSa) timePeriod += 'Sat,';
    if (vals.buDaysSu) timePeriod += 'Sun,';
    if (timePeriod != '') timePeriod = timePeriod.slice(0, -1);
    timePeriod += `;${vals.buTime}`;

    this.selected.alias = this.form.value.confAlias;
    this.selected.packageCount = this.form.value.count;
    this.selected.packageSize = this.form.value.size;
    this.selected.backupType = +this.form.value.SaveType;
    this.selected.timePeriod = timePeriod;
    this.selected.email = this.form.value.email;
    this.selected.mailTimeCron =
      this.form.value.mailTimeCron === '59 59 23 31 12 ? 2099'
        ? '59 59 23 31 12 ? 2099'
        : timePeriodToCron(timePeriod);

    console.warn(this.selected);

    this.saveBtnText = '✉ sending...'
    await this.configsService.put(this.selected.id, this.selected);
    this.saveBtnText = 'Saved ✔'
    setTimeout(() => {this.saveBtnText = 'Save changes'}, 2500)


  }

  public deleteConf(): void {
    this.configsService.delete(this.selected.id);
  }

  private createForm(): FormGroup {
    const vals = this.selected;
    return this.fb.group({
      count: [vals?.packageCount || 1, [Validators.min(1)]],
      size: [vals?.packageSize || 1, [Validators.min(1)]],
      SaveType: '' + vals?.backupType,
      buDaysMo: vals?.timePeriod.includes('Mon'),
      buDaysTu: vals?.timePeriod.includes('Tue'),
      buDaysWe: vals?.timePeriod.includes('Wed'),
      buDaysTh: vals?.timePeriod.includes('Thu'),
      buDaysFr: vals?.timePeriod.includes('Fri'),
      buDaysSa: vals?.timePeriod.includes('Sat'),
      buDaysSu: vals?.timePeriod.includes('Sun'),
      buTime: vals?.timePeriod.split(';')[1],
      confAlias: [vals?.alias, [Validators.minLength(3)]],
      // prettier-ignore
      sourcePath: ['', [Validators.pattern('^([A-Za-z]:)((\/|\/\/)[A-Za-z_\-\s0-9\.]+)+$')]],
      email: [vals.email, [Validators.email]],
      mailTimeCron:
        vals.mailTimeCron === '59 59 23 31 12 ? 2099'
          ? '59 59 23 31 12 ? 2099'
          : 'sameAsBU',
    });
  }

  get count() {
    return this.form.get('count');
  }

  get size() {
    return this.form.get('size');
  }

  get email() {
    return this.form.get('email');
  }

  get confAlias() {
    return this.form.get('confAlias');
  }

  get sourcePath() {
    return this.form.get('sourcePath');
  }
}
