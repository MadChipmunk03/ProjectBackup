import { Injectable } from '@angular/core';
import { Config } from '../models/config.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ConfigsService {
  constructor(
    private router: Router,
    private http: HttpClient,
    private sessions: SessionsService
  ) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public save(config: Config): void {
    this.http
      .post<Config>(environment.api + '/api/Configs/', config, this.options)
      .subscribe((output) => {
        console.log(output);
        this.router.navigate([`config/${output.id}`]);
      });
  }

  public put(id: number, config: Config): void {
    this.http
      .put<Config>(environment.api + '/api/Configs/' + id, config, this.options)
      .subscribe((output) => {
        console.log(output);
      });
  }

  public delete(id: number): void {
    this.http
      .delete(environment.api + '/api/Configs/' + id, this.options)
      .subscribe(() => this.router.navigate(['configs']));
  }

  public findAll(): Observable<Config[]> {
    return this.http.get<Config[]>(
      environment.api + '/api/Configs/',
      this.options
    );
  }

  public findById(id: number): Observable<Config> {
    return this.http.get<Config>(
      environment.api + '/api/Configs/' + id,
      this.options
    );
  }
}
