import { Component, OnInit } from '@angular/core';

interface RentalEntry{
  contractID: number;
  bookName: string;
  startDate: string;
  endDate: string;
  branchName: string;
  renterID: number;
  ownerID: number;
  isPending: boolean;
}
interface ListingEntry{
  contractID: number;
  bookName: string;
  startDate: string;
  endDate: string;
  branchName: string;
  ownerID: number;
  isPending: boolean;
}

@Component({
  selector: 'app-librarian-page',
  templateUrl: './librarian-page.component.html',
  styleUrls: ['./librarian-page.component.css']
})
export class LibrarianPageComponent implements OnInit {
  displayedColumns1 = ['contractID', 'bookName', 'startDate', 'endDate', 'branchName', 'renterID', 'ownerID', 'isPending'];
  displayedColumns2 = ['contractID', 'bookName', 'startDate', 'endDate', 'branchName', 'ownerID', 'isPending'];
  dataSource1: RentalEntry[] = [];
  dataSource2: ListingEntry[] = [];


  constructor() { }

  ngOnInit(): void {
    this.dataSource1 = [
      {
        contractID: 1,
        bookName: 'War and Peace',
        startDate: '10/10/2020',
        endDate: '31/11/2020',
        branchName: 'LAU Jbeil',
        renterID: 123,
        ownerID: 10,
        isPending: false,
      },
      {
        contractID: 2,
        bookName: 'The Great Gatsby',
        startDate: '09/06/2020',
        endDate: '30/10/2020',
        branchName: 'LAU Jbeil',
        renterID: 123456,
        ownerID: 10456,
        isPending: true,
      },
      {
        contractID: 3,
        bookName: 'Lord of the Rings',
        startDate: '13/05/2020',
        endDate: '08/07/2020',
        branchName: 'LAU Beirut',
        renterID: 145623,
        ownerID: 56410,
        isPending: false,
      }];
    this.dataSource2 = [
      {
        contractID: 1,
        bookName: 'War and Peace',
        startDate: '10/10/2020',
        endDate: '31/11/2020',
        branchName: 'LAU Jbeil',
        ownerID: 10,
        isPending: false,
      },
      {
        contractID: 2,
        bookName: 'The Great Gatsby',
        startDate: '09/06/2020',
        endDate: '30/10/2020',
        branchName: 'LAU Jbeil',
        ownerID: 10456,
        isPending: true,
      },
      {
        contractID: 3,
        bookName: 'Lord of the Rings',
        startDate: '13/05/2020',
        endDate: '08/07/2020',
        branchName: 'LAU Beirut',
        ownerID: 56410,
        isPending: false,
      }];
  }

}
