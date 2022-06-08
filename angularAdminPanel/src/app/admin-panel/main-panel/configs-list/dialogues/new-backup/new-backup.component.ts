import { Component, OnInit } from '@angular/core';
import { Config } from 'src/app/models/config.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigsService } from 'src/app/services/configs.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { timePeriodToCron } from 'src/app/helper';

@Component({
  selector: 'app-new-backup',
  templateUrl: './new-backup.component.html',
  styleUrls: ['./new-backup.component.scss'],
})
export class NewBackupComponent implements OnInit {
  public form: FormGroup;
  public newConfig: Config;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private http: HttpClient,
    private confService: ConfigsService
  ) {}

  ngOnInit(): void {
    this.form = this.createForm();
    this.newConfig = new Config();
  }

  public addConfig(): void {
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
    timePeriod != ''
      ? (timePeriod = timePeriod.slice(0, -1))
      : (this.form.value.mailTimeCron = '59 59 23 31 12 ? 2099');
    timePeriod += `;${vals.buTime}`;

    this.newConfig.packageCount = this.form.value.count;
    this.newConfig.packageSize = this.form.value.size;
    this.newConfig.backupType = +this.form.value.SaveType;
    this.newConfig.timePeriod = timePeriod;
    this.newConfig.email = this.form.value.email;
    this.newConfig.mailTimeCron =
      this.form.value.mailTimeCron === '59 59 23 31 12 ? 2099'
        ? '59 59 23 31 12 ? 2099'
        : timePeriodToCron(timePeriod);

    console.warn(this.newConfig);

    this.confService.save(this.newConfig);

    // window.location.reload();
  }

  public escape(): void {
    window.location.reload();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      count: [1, [Validators.min(1)]],
      size: [1, [Validators.min(1)]],
      SaveType: '1',
      buDaysMo: false,
      buDaysTu: false,
      buDaysWe: false,
      buDaysTh: false,
      buDaysFr: false,
      buDaysSa: false,
      buDaysSu: false,
      buTime: '12:00',
      email: ['', [Validators.required, Validators.email]],
      mailTimeCron: 'sameAsBU',
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
}
