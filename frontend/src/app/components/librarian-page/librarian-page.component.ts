import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

interface RentalEntry{
bookName: string; startDate: string; endDate: string; branchName: string; userName: string;
}

@Component({
  selector: 'app-librarian-page',
  templateUrl: './librarian-page.component.html',
  styleUrls: ['./librarian-page.component.css']
})
export class LibrarianPageComponent implements OnInit {
  displayedColumns1 = ['bookName', 'startDate', 'endDate', 'branchName', 'renterID'];
  displayedColumns2 = ['bookName', 'startDate', 'endDate', 'branchName', 'ownerID'];
  dataSource1: RentalEntry[] = [];
  dataSource2: RentalEntry[] = [];


  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    this.http.get<{bookName: string, startDate: string, endDate: string, branchName: string, userName: string}[]>('http://localhost:5000/Librarian/GetRentals').subscribe(h => {
      this.dataSource1 = h;
      console.log(h);
    });

    this.http.get<{bookName: string, startDate: string, endDate: string, branchName: string, userName: string}[]>('http://localhost:5000/Librarian/GetOwners').subscribe(h => {
      this.dataSource2 = h;
      console.log(h);
    });
  }

  clearRentals(): void {
    this.http.get('http://localhost:5000/Librarian/ClearRentals').subscribe(h => {
    });
  }

  clearOwners(): void {
    this.http.get('http://localhost:5000/Librarian/ClearOwners').subscribe(h => {
    });
  }

}
