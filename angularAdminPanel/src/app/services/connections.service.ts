import { Injectable } from '@angular/core';
import { Connection } from '../models/connection.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';

@Injectable({
  providedIn: 'root',
})
export class ConnectionsServices {
  constructor(private http: HttpClient, private sessions: SessionsService) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public findAll(): Observable<Connection[]> {
    return this.http.get<Connection[]>(
      environment.api + '/api/Connections/',
      this.options
    );
  }

  public findById(id: number): Observable<Connection> {
    return this.http.get<Connection>(
      environment.api + '/api/' + id,
      this.options
    );
  }

  public findDeamsCon(id: number): Observable<Connection[]> {
    return this.http.get<Connection[]>(
      environment.api + '/api/Connections/deamon/' + id,
      this.options
    );
  }

  public findConfsCon(id: number): Observable<Connection[]> {
    return this.http.get<Connection[]>(
      environment.api + '/api/Connections/config/' + id,
      this.options
    );
  }

  public put(conn: Connection): void{
    this.http
      .put<Connection>(environment.api + '/api/Connections/' + conn.id, conn, this.options)
      .subscribe((output) => {
        console.log(output);
      });
  }
}
