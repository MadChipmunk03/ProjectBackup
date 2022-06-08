import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Config } from 'src/app/models/config.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigsService } from 'src/app/services/configs.service';
import {
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Destination } from 'src/app/models/destination.model';
import { DestinationsServices } from 'src/app/services/destinations.service';

@Component({
  selector: 'app-dia-destination',
  templateUrl: './dia-destination.component.html',
  styleUrls: ['./dia-destination.component.scss'],
})
export class DiaDestinationComponent implements OnInit {
  @Output() activeDia = new EventEmitter<string>();
  @Output() addDest = new EventEmitter<Destination>();
  @Input() selectedDia = '';

  public form: FormGroup;
  public formReady: boolean = false;
  public newDest: Destination;
  private destId: number;

  constructor(
    private destService: DestinationsServices,
    private router: Router,
    private fb: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute,
    private confService: ConfigsService
  ) {}

  ngOnInit(): void {
    this.form = this.createForm();
    this.newDest = new Destination();
    this.newDest.configId = +this.route.snapshot.params['id'];
    this.destId = Number(this.selectedDia.split(';')[1]);
    this.updateForm();
  }

  public escape(): void {
    this.activeDia.emit('');
  }

  public addDestination(): void {
    const vals = this.form.value;

    this.newDest.saveType = Number(vals.saveType);
    this.newDest.path = vals.path;
    this.newDest.iP_Address = vals.IP;
    this.newDest.username = vals.username;
    this.newDest.password = vals.password;

    if (this.destId)
      this.destService.put(this.destId, this.newDest).subscribe((data) => {
        this.addDest.emit(data);
        this.activeDia.emit('');
      });
    else
      this.destService.post(this.newDest).subscribe((data) => {
        this.addDest.emit(data);
        this.activeDia.emit('');
      });
  }

  public updateForm(): void {
    if (!this.destId) {
      this.formReady = true;
      return;
    }
    this.destService.findDestById(this.destId).subscribe((data) => {
      this.form.setValue({
        saveType: String(data.saveType),
        path: data.path,
        IP: data.iP_Address,
        username: data.username,
        password: data.password,
      });
      console.log(this.form.value);
      this.formReady = true;
    });
  }

  private createForm(): FormGroup {
    return this.fb.group({
      saveType: '0',
      // prettier-ignore
      path: ['', [Validators.required]], //Validators.pattern(this.pathValidator || ''),
      IP: [
        '',
        [
          Validators.pattern(
            // prettier-ignore
            '^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$'
          ),
        ],
      ],
      username: '',
      password: '',
    });
  }

  get pathValidator() {
    return +this.form.value.saveType == 0
      ? '^([A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$'
      : '^(ftp:|FTP://|[A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$';
    const type = +this.form.value.saveType;
    console.log(type);
    switch (type) {
      case 0:
        // return 'foo';
        return '^([A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$';
      case 1:
        // return 'bar';
        return '^([A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$';
      case 2:
        // return 'baz';
        return '^(ftp:|FTP://|[A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$';
      default:
        return '^([A-Za-z]:)((/|//)[A-Za-z_-s0-9.]+)+$';
    }
  }

  get path() {
    return this.form.get('path');
  }

  get IP() {
    return this.form.get('IP');
  }
}
