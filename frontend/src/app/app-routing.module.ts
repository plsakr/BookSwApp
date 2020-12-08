import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RegisterUserComponent} from './register-user/register-user.component';
import {LibraryComponent} from './library/library.component';
import {BookComponent} from './book/book.component';
import {CartComponent} from './components/shopping-cart/cart/cart.component';
import {UserComponent} from './user/user.component';
import {BookListingComponent} from './components/book-listing/book-listing.component';

const routes: Routes = [
  {path: 'login', component: RegisterUserComponent},
  {path: 'library', component: LibraryComponent},
  {path: 'book', component: BookComponent},
  {path: 'cart', component: CartComponent},
  {path: 'user', component: UserComponent},
  {path:'listing', component:BookListingComponent},
  {path: '**', component: UserComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
