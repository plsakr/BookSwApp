import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RegisterUserComponent} from './register-user/register-user.component';
import {LibraryComponent} from './library/library.component';
import {BookComponent} from './book/book.component';
import {CartComponent} from './components/shopping-cart/cart/cart.component';
import {UserComponent} from './user/user.component';
import {AuthGuardService} from './services/auth-guard.service';

const routes: Routes = [
  {path: 'login', component: RegisterUserComponent},
  {path: 'library', component: LibraryComponent, canActivate: [AuthGuardService], data: {expectedRoles: ['USER']}},
  {path: 'book', component: BookComponent, canActivate: [AuthGuardService], data: {expectedRoles: ['USER']}},
  {path: 'cart', component: CartComponent, canActivate: [AuthGuardService], data: {expectedRoles: ['USER']}},
  {path: 'user', component: UserComponent, canActivate: [AuthGuardService], data: {expectedRoles: ['USER', 'LIBRARIAN']}},
  {path: '**', component: RegisterUserComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
