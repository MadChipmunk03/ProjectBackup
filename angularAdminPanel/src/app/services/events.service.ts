import { Injectable } from '@angular/core';
import { Event } from '../models/event.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  public TypeToStr(type: number): string{
    if (type === 0) return '✅';
    else if (type === 1) return '⛔';
    else if (type === 2) return '📣';
    return '';
  }

  constructor(private http: HttpClient, private sessions: SessionsService) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public findAll(): Observable<Event[]> {
    return this.http.get<Event[]>(environment.api + '/api/Events/', this.options);
  }

  public findById(id: number): Observable<Event> {
    return this.http.get<Event>(environment.api + '/api/Events/' + id, this.options);
  }



  // private events: Event[] = [
  //   {
  //     Id: 1,
  //     connectionId: 0,
  //     message: 'Deamon unreachable',
  //     time: '1.1.2022_12:54',
  //     status: true,
  //     type: 1,
  //   },
  //   {
  //     Id: 2,
  //     connectionId: 1,
  //     message: 'storageFull',
  //     time: '1.1.2022_12:54',
  //     status: true,
  //     type: 1,
  //   },
  //   {
  //     Id: 3,
  //     connectionId: 0,
  //     message: 'storageFull',
  //     time: '1.3.2022_12:54',
  //     status: false,
  //     type: 2,
  //   },
  //   {
  //     Id: 4,
  //     connectionId: 0,
  //     message: 'Deamon disconnected',
  //     time: '1.5.2022_12:54',
  //     status: true,
  //     type: 2,
  //   },
  //   {
  //     Id: 5,
  //     connectionId: 0,
  //     message: 'backup succesful',
  //     time: '1.7.2022_12:54',
  //     status: true,
  //     type: 0,
  //   },
  // ];

  // // public findAll(): Event[] {
  // //   return this.events;
  // // }

  // constructor() {}
}
