import { Injectable } from '@angular/core';
import { Deamon } from '../models/deamon.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class DeamonsService {
  public statusToStr(stat: number): string {
    switch (stat) {
      case 0:
        return '⛔unsync';
      case 1:
        return '✅synced';
      case 2:
        return '⏸disabled';
      case 3:
        return '⏸disabled';
      default:
        return '💥unknown';
    }
  }
  constructor(
    private router: Router,
    private http: HttpClient,
    private sessions: SessionsService,
  ) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public put(deamon: Deamon): void {
    this.http
      .put<Deamon>(
        environment.api + '/api/Deamons/' + deamon.id,
        deamon,
        this.options
      )
      .subscribe((output) => {
        console.log(output);
      });
  }

  public delete(id: number): void {
    this.http
      .delete(environment.api + '/api/Deamons/' + id, this.options)
      .subscribe(() => this.router.navigate(['deamons']));
  }

  public findAll(): Observable<Deamon[]> {
    return this.http.get<Deamon[]>(
      environment.api + '/api/Deamons/',
      this.options
    );
  }

  public findById(id: number): Observable<Deamon> {
    return this.http.get<Deamon>(
      environment.api + '/api/Deamons/' + id,
      this.options
    );
  }
}
