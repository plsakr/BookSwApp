import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {ApplicationUser, AuthService} from './auth.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate{
  currentUser: ApplicationUser;
  constructor(private auth: AuthService, private router: Router) {
    this.currentUser = {id: -1, role: ''};
    this.auth.user$.subscribe(x => {
      this.currentUser = x;
    });
  }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    console.log('am i logged in?');
    const expectedRoles = route.data.expectedRoles;
    const canGo = this.hasExpectedRole(expectedRoles);
    return canGo.pipe(map(x => {
      if (!x) {
        this.router.navigate(['login']);
      }
      return x;
    }));
  }

  hasExpectedRole(role: string[]): Observable<boolean> {
    return this.auth.testRole().pipe(map(x => role.includes(x)));
  }
}
