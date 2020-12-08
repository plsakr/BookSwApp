import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {catchError, map} from 'rxjs/operators';


export interface ApplicationUser {
  id: number;
  role: string;
}

@Injectable()
export class AuthService {
  private readonly authURL = `http://localhost:5000/User`;
  private user = new BehaviorSubject<ApplicationUser>({id: -1, role: 'NONE'});
  user$: Observable<ApplicationUser> = this.user.asObservable();

  constructor(private router: Router, private http: HttpClient) {
  }

  login(userName: string, password: string, onError: () => void): void {
    this.http.post<ApplicationUser>(`${this.authURL}/login`,
      {email: userName, password},
      {withCredentials: true, observe: 'response'}).subscribe(h => {
      console.log(h);
      if (h.ok) {
        if (h.body != null) {
          this.user.next({id: h.body.id, role: h.body.role});
        }
      }
    }, () => {
      onError();
    });
  }
  logout(): Observable<boolean> {
    return this.http.get(`${this.authURL}/logout`, {withCredentials: true, observe: 'response'})
      .pipe(map(h => {
        if (h.ok) {
          this.user.next({id: -1, role: ''});
          return true;
        }
        return false;
      }));
  }
  testLogin(): void {
    console.log('calling test');
    this.http.get<ApplicationUser>(`${this.authURL}/test`,
      {withCredentials: true, observe: 'response'}).subscribe(h => {
       if (h.ok) {
         if (h.body != null) {
           console.log('test complete');
           this.user.next({id: h.body.id, role: h.body.role});
         }
       }
    });
  }

  testRole(): Observable<string> {
    return this.http.get<ApplicationUser>(`${this.authURL}/test`,
      {withCredentials: true, observe: 'response'}).pipe(map(x => {
        if (x.ok) {
          const b = x.body;
          if (b != null) {
            if (b.id !== this.user.getValue().id || b.role !== this.user.getValue().role) {
              this.user.next(b);
            }
            return b.role;
          } else {
            return '';
          }
        } else {
          return '';
        }
      }), catchError(err => {
        return of('') as Observable<string>;
    }));
  }

  register(name: string, password: string, email: string): void {
    this.http.post(`${this.authURL}/register`, {name, password, email}, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }

  test(): void{
    this.http.get(`${this.authURL}/test`, {withCredentials: true}).subscribe(h => {
      console.log(h);
    });
  }
}
