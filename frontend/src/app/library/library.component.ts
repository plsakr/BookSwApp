import {Component, OnInit, ViewChild} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatAutocomplete} from '@angular/material/autocomplete';
import {HttpClient} from '@angular/common/http';
import {MatRadioChange} from '@angular/material/radio';
import {MatSelectChange} from '@angular/material/select';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';


type Book = {
  title: string,
  isbn: string,
  author: string,
  image: string,
  shortDesc: string
};


type tag = {
  tagName: string,
  bookISBN: string
};
type BookResult = {
  author: string,
  genre: string,
  isbn: string,
  name: string,
  publisher: string,
  releaseDate: string,
  tags: tag[]
};

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {
  mySearch = new FormControl();
  filteredOptions: Observable<string[]> | undefined;

  value = '';
  genres = ['All'];
  availableTags: string[] | undefined;
  tagsControl = new FormControl([]);
  selectedTags: string[] = [];
  pageSize = 10;
  currentPage = 0;
  totalSize = 0;
  pageEvent: PageEvent | undefined;

  // TEST THINGS
  HarryPotter: Book = {title: 'Harry Potter and the Half-Blood Prince', author: 'JK Rowling', isbn: '0123456789',
    image: 'https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1587697303l/1._SX318_.jpg',
  shortDesc: 'This is my short description. Hello!'};
  fullData = new Array(100).fill(this.HarryPotter);
  selectedGenre = 'All';
  selectedSorting = 'name';

  gridColumns = 6;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  currentData: BookResult[] = [];

  sortedData: BookResult[] = [];
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    const allNames = this.sortedData.map(x => x.name);
    const allAuthors = this.sortedData.map(x => x.author);
    const finalData: string[] = [];
    allNames.forEach((x, i) => finalData.push(x));
    allAuthors.forEach((x, i) => {
      if (!finalData.includes(x)) { finalData.push(x); }
    });
    return finalData.filter(option => option.toLowerCase().includes(filterValue));
  }

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    // get fullData first
    // inside getting data:
    // this.totalSize = this.fullData.length;
    // this.iterator();
    this.filteredOptions = this.mySearch.valueChanges
      .pipe(
        startWith(''),
        map((value: string) => this._filter(value))
      );
    this.httpClient.get<BookResult[]>('http://localhost:5000/Book').subscribe(h => {
      console.log(h);
      this.fullData = h;
      this.iterator();
      h.forEach(b => {
        b.tags.forEach(t => {
          if (this.availableTags === undefined) {
            this.availableTags = [t.tagName];
          }else {
            if (!this.availableTags.includes(t.tagName)) {
              this.availableTags.push(t.tagName);
            }
          }
        });
        if (!this.genres.includes(b.genre)) {
          this.genres.push(b.genre);
        }
      });
    });
  }

  onSearch(): void {
    this.iterator();
  }

  onSearchClear(): void {
    this.value = '';
    this.iterator();
  }

  radioChange($event: MatRadioChange): void {
    this.selectedSorting = $event.value;
    this.iterator();
  }

  genreChange($event: MatSelectChange): void {
    this.iterator();
  }

  tagChange($event: MatSelectChange): void {
    this.iterator();
  }
  handlePage(e: any): PageEvent {
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.iterator();
    return e;
  }

  onBookView(i: string): void {
    console.log('HELLO');
    this.router.navigate(['/book', {isbn: i}]);
  }

  getSortedData(): BookResult[] {
    let firstOut: BookResult[] = [];
    let secondOut: BookResult[] = [];

    if (this.selectedGenre === 'All'){
      firstOut = this.fullData;
    } else {
      firstOut = this.fullData.filter((b, i) => b.genre === this.selectedGenre);
    }
    if (this.selectedTags.length === 0) {
      secondOut = firstOut;
    } else {
      secondOut = firstOut.filter((b, i) => this.selectedTags.every(t => b.tags.map(t1 => t1.tagName).includes(t)));
    }
    if (this.selectedSorting === 'name') {
      return secondOut.sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase()) ? 1 :
        ((b.name.toLowerCase() > a.name.toLowerCase()) ? -1 : 0));
    } else if (this.selectedSorting === 'author') {
      return secondOut.sort((a, b) => (a.author.toLowerCase() > b.author.toLowerCase()) ? 1 :
        ((b.author.toLowerCase() > a.author.toLowerCase()) ? -1 : 0));
    } else {
      return secondOut.sort((a, b) =>
        // tslint:disable-next-line:radix
        (parseInt(a.releaseDate.split('-')[0]) > parseInt(b.releaseDate.split('-')[0])) ? 1 :
          // tslint:disable-next-line:radix
           ((parseInt(b.releaseDate.split('-')[0]) > parseInt(a.releaseDate.split('-')[0])) ? -1 : 0));
    }
  }

  iterator(): void {
    const end = (this.currentPage + 1) * this.pageSize;
    const start = this.currentPage * this.pageSize;
    this.sortedData = this.getSortedData();
    if (this.value !== '')
    {
      const search = this.value.toLowerCase();
      this.sortedData = this.sortedData.filter((b, i) => b.name.toLowerCase().includes(search) || b.author.toLowerCase().includes(search));
    }
    this.totalSize = this.sortedData.length;
    const part = this.sortedData.slice(start, end);
    this.currentData = part;
  }

  onTagRemoved(myTag: string): void {
    const tags = this.tagsControl.value as string[];
    this.removeFirst(tags, myTag);
    this.tagsControl.setValue(['']);
    this.tagsControl.setValue(tags);
    console.log(this.selectedTags);
    this.iterator();
  }

  private removeFirst<T>(array: T[], toRemove: T): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }


}
