import { Injectable } from '@angular/core';
import { Source } from '../models/source.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SessionsService } from './sessions.service';

@Injectable({
  providedIn: 'root',
})
export class SourcesService {
  constructor(private http: HttpClient, private sessions: SessionsService) {}

  public get options(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Authorization: '' + this.sessions.token,
      }),
    };
  }

  public post(source: Source): Observable<Source>{
    return this.http.post<Source>(environment.api + '/api/sources/', source, this.options)
  }

  public findById(id: number): Observable<Source[]> {
    return this.http.get<Source[]>(environment.api + '/api/Sources/' + id, this.options);
  }

  public async delete(id: number): Promise<void> {
    this.http.delete(environment.api + '/api/Sources/' + id, this.options).subscribe(data => console.log(data));
  }
}
