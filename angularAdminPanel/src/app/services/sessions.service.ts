import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class SessionsService {
  public token: string | null;

  constructor(private http: HttpClient, private jwt: JwtHelperService) {
    this.token = this.loadToken();
  }

  public login(password: string): Observable<boolean> {
    return this.http
      .post<string>(
        `${environment.api}/api/sessions?password=${password}`,
        undefined
      )
      .pipe(
        tap((token) => (this.token = token)),
        tap((token) => this.saveToken(token)),
        map(() => true),
        catchError(() => of(false))
      );
  }

  public logout(): void {
    this.token = null;
    sessionStorage.removeItem('token');
  }

  public get authenticated(): boolean {
    return !!this.token && !this.jwt.isTokenExpired(this.token);
  }

  private saveToken(token: string): void {
    sessionStorage.setItem('token', token);
  }

  private loadToken(): string | null {
    return sessionStorage.getItem('token');
  }
}
