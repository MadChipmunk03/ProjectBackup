import { Injectable } from '@angular/core';
import { Destination } from '../models/destination.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';

@Injectable({
  providedIn: 'root',
})
export class DestinationsServices {

  public saveTypeToStr(saveType: number): string{
    switch (saveType) {
      case 0:
        return 'LOC';
      case 1:
        return 'NET';
      case 2:
        return 'FTP';
      default:
        return '💥unknown';
    }
  }
  constructor(private http: HttpClient, private sessions: SessionsService) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public post(dest: Destination): Observable<Destination>{
    return this.http.post<Destination>(environment.api + '/api/destinations/', dest, this.options)
  }

  public put(id: number, dest: Destination): Observable<Destination> {
    return this.http.put<Destination>(environment.api + '/api/destinations/' + id, dest, this.options);
  }

  //array of destinations refered to certaing config
  public findById(id: number): Observable<Destination[]> {
    return this.http.get<Destination[]>(environment.api + '/api/Destinations/' + id, this.options);
  }

  public delete(id: number): void {
      this.http.delete(environment.api + '/api/Destinations/' + id, this.options).subscribe(data => console.log(data));
  }

  public findDestById(id: number): Observable<Destination> {
    return this.http.get<Destination>(environment.api + '/api/Destinations/dest/' + id, this.options);
  }
}
