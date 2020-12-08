import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl, FormBuilder} from '@angular/forms';
@Component({
  selector: 'app-book-listing',
  templateUrl: './book-listing.component.html',
  styleUrls: ['./book-listing.component.css']
})

export class BookListingComponent implements OnInit {
  dateForm: FormGroup;
  checkoutForm;
  // ISBN: number;
  // bookName: string;
  // authorName: string;
  // genre: string;
  // releaseDate: Date;
  // publisher: string;
  submitForm(customerData){
    // Process checkout data here
    console.log("lalala");

    console.log(this.checkoutForm);
    console.log("Your ISBN is:",this.checkoutForm.controls['ISBN'].value)
    console.log("Your Book Name is:",this.checkoutForm.controls['bookName'].value)
    console.log("Your Author Name is:",this.checkoutForm.controls['authorName'].value)
    console.log("Your Genre is:",this.checkoutForm.controls['genre'].value)
    console.log("Your Release Date is:",this.checkoutForm.controls['releaseDate'].value)
    console.log("Your Publisher is:",this.checkoutForm.controls['publisher'].value)
    console.log("Your start date is:",this.checkoutForm.controls['startDate'].value)
    console.log("Your end date is:",this.checkoutForm.controls['endDate'].value)
    this.checkoutForm.reset();
    console.warn('Your order has been submitted', customerData);
  }
  constructor(private formBuilder: FormBuilder) {
    // let ISBN= this.ISBN;
    // let bookName= this.bookName ;
    // let authorName= this.authorName;
    // let genre= this.genre;
    // let releaseDate= this.releaseDate;
    // let publisher= this.ISBN;
    this.checkoutForm = this.formBuilder.group({
      ISBN:undefined,
      bookName:"",
      authorName:"",
      genre:"",
      releaseDate: undefined,
      publisher:"",
      startDate: undefined,
      endDate: undefined,
    });
    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();
    const day = today.getDate();
    this.dateForm = new FormGroup({
      start: new FormControl(new Date(year, month, day)),
      end: new FormControl(new Date(year, month, day))
    })
    
   }

  ngOnInit(): void {
  }
  

}
