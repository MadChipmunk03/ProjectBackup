import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SessionsService } from 'src/app/services/sessions.service';
import { Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public form: FormGroup;

  public WrongPassword: boolean = false;

  public login(): void {
    console.log(this.form.value.password);
    this.service
      .login(this.form.value.password)
      .pipe(
        filter((result) => {
          this.WrongPassword = !result;
          console.log('🈹', result, this.WrongPassword);
          
          return result === true;
        })
      )
      .subscribe(() => this.router.navigate(['/']));
  }

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: SessionsService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      password: ['', [Validators.required]],
    });
  }

  get password() {
    return this.form.get('password');
  }
}
