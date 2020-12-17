import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  books: {copyId: number, bookName: string}[] = [];

  delete(copyId: number): void {
    this.http.get(`http://localhost:5000/Cart/delete?copyId=${copyId}`, {withCredentials: true}).subscribe(x => {
      this.route.navigate(['/cart']);
    });
  }

  constructor(private http: HttpClient, private route: Router) {

  }

  ngOnInit(): void {
    this.http.get<{copyId: number, bookName: string}[]>('http://localhost:5000/Cart/get', {withCredentials: true}).subscribe(x => {
      this.books = x;
    });

  }

}
