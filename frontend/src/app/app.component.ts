import { Component } from '@angular/core';
import {AuthService} from './services/auth.service';
import {Router} from '@angular/router';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Book SwApp';
  search = false;

  constructor(private auth: AuthService, private router: Router) {
  }

  logout(): void {
    this.auth.logout().pipe(first()).subscribe(x => {
      if (x) {
        console.log('IM BEING STUPID');
        this.router.navigate(['login']);
      }
    });
  }
  user(): void {
    this.router.navigate(['user']);
  }
  checkout(): void {
    this.router.navigate(['cart']);
    console.log('cart');
  }

  add(): void {
    this.router.navigate(['listing']);
  }

  home(): void {
    this.router.navigate(['']);
  }
}
