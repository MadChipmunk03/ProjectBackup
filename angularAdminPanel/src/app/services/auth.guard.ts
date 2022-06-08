import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import { SessionsService } from "./sessions.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate
{
  constructor(private router: Router,
              private sessions: SessionsService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.sessions.authenticated)
      this.router.navigate([ '/login' ]);

    return this.sessions.authenticated;
  }

}