import {Injectable, OnDestroy} from '@angular/core';
import {BehaviorSubject, Observable, Subscription} from 'rxjs';
import {Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';

interface ApplicationUser {
  email: string;
  role: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authURL = `http://localhost:5000/User`;
  private user = new BehaviorSubject<ApplicationUser>({email: '', role: 'NONE'});
  user$: Observable<ApplicationUser> = this.user.asObservable();

  constructor(private router: Router, private http: HttpClient) {
  }

  // NEED TO RETURN IF SUCCESSFUL
  login(userName: string, password: string): void {
    this.http.post(`${this.authURL}/login`, {email: userName, password}, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }

  register(name: string, password: string, email: string): void {
    this.http.post(`${this.authURL}/register`, {name, password, email}, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }

  logout(): void {
    this.http.post(`${this.authURL}/logout`, {}, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }

  test(): void{
    this.http.get(`${this.authURL}/test`, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }
}
