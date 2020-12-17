import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormGroup, FormControl, FormBuilder} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import {Router} from '@angular/router';


type BookResult = {
  author: string,
  genre: string,
  isbn: string,
  name: string,
  publisher: string,
  releaseDate: string,
  tags: tag[]
};

type tag = {
  tagName: string,
  bookISBN: string
};

@Component({
  selector: 'app-book-listing',
  templateUrl: './book-listing.component.html',
  styleUrls: ['./book-listing.component.css']
})
export class BookListingComponent implements OnInit {
  dateForm: FormGroup;
  checkoutForm;
  isFound = false;
  // public foundISBN: Observable<boolean> = this.isFound.asObservable();
  name = '';
  author = '';
  genre = '';
  date = '';
  publisher = '';
  // ISBN: number;
  // bookName: string;
  // authorName: string;
  // genre: string;
  // releaseDate: Date;
  // publisher: string;
  submitForm(customerData: any): void{
    // Process checkout data here
    console.log('lalala');
    // I want to send this data to the database
    console.log(this.checkoutForm);
    if (!this.isFound) {
      const post = {
        Isbn: this.checkoutForm.controls.ISBN.value,
        EndDate: this.checkoutForm.controls.endDate.value,
        branchID: 1,
        Name: this.checkoutForm.controls.bookName.value,
        Author: this.checkoutForm.controls.authorName.value,
        Genre: this.checkoutForm.controls.genre.value,
        ReleaseDate: this.checkoutForm.controls.releaseDate.value,
        Publisher: this.checkoutForm.controls.publisher.value
      };
      this.http.post(`http://localhost:5000/OwnerContract/addBookCopy`, post, {withCredentials: true}).subscribe(h => {
        this.checkoutForm.reset();
        this.router.navigate(['']);
      });
    } else {
      const post = {
        Isbn: this.checkoutForm.controls.ISBN.value,
        EndDate: this.checkoutForm.controls.endDate.value,
        branchID: 1,
        Name: this.name,
        Author: this.author,
        Genre: this.genre,
        ReleaseDate: this.date,
        Publisher: this.publisher,
      };
      this.http.post(`http://localhost:5000/OwnerContract/addBookCopy`, post, {withCredentials: true}).subscribe(h => {
        this.checkoutForm.reset();
        this.router.navigate(['']);
      });
    }

    this.checkoutForm.reset();
    console.warn('Your order has been submitted', customerData);
  }
  constructor(private formBuilder: FormBuilder, private http: HttpClient, private cdRef: ChangeDetectorRef, private router: Router) {
    // let ISBN= this.ISBN;
    // let bookName= this.bookName ;
    // let authorName= this.authorName;
    // let genre= this.genre;
    // let releaseDate= this.releaseDate;
    // let publisher= this.ISBN;
    this.checkoutForm = this.formBuilder.group({
      ISBN: undefined,
      bookName: '',
      authorName: '',
      genre: '',
      releaseDate: undefined,
      publisher: '',
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
    });

   }

   onISBNChange(): void {
      const isbn = this.checkoutForm.controls.ISBN.value;
      this.http.get<BookResult | null>(`http://localhost:5000/book/byId?id=${isbn}`).subscribe(h => {
        console.log(h);
        if (h == null)
        {
          this.isFound = false;
        } else {
          this.isFound = true;
          this.name = h.name;
          this.author = h.author;
          this.genre = h.genre;
          this.publisher = h.publisher;
          this.date = h.releaseDate;
        }
        this.cdRef.detectChanges();
      });
   }

  ngOnInit(): void {
  }


}
