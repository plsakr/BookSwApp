import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  onCheckout(): void {
    this.http.get('http://localhost:5000/RentalContract/checkout', {withCredentials: true}).subscribe(h => {
      this.router.navigate(['/']);
    });

  }

}
