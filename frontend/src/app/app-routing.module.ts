import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RegisterUserComponent} from './register-user/register-user.component';
import {LibraryComponent} from './library/library.component';
import {BookComponent} from './book/book.component';
import {CartComponent} from './components/shopping-cart/cart/cart.component';
import {UserComponent} from './user/user.component';
import {AuthGuardService} from './services/auth-guard.service';
import {BookListingComponent} from './components/book-listing/book-listing.component';
import {LibrarianPageComponent} from './components/librarian-page/librarian-page.component';

const routes: Routes = [
  {path: 'login', component: RegisterUserComponent},
  {path: 'library', component: LibraryComponent, data: {expectedRoles: ['USER']}},
  {path: 'book', component: BookComponent,  data: {expectedRoles: ['USER']}},
  {path: 'cart', component: CartComponent, data: {expectedRoles: ['USER']}},
  {path: 'listing', component: BookListingComponent, data: {expectedRoles: ['USER']}},
  {path: 'user', component: UserComponent, data: {expectedRoles: ['USER', 'LIBRARIAN']}},
  {path: 'librarian', component: LibrarianPageComponent, data: {expectedRoles: ['LIBRARIAN']}},
  {path: '**', component: RegisterUserComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
