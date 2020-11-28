import {Component, OnInit, ViewChild} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';


type Book = {
  title: string,
  isbn: string,
  author: string,
  image: string,
  shortDesc: string
};

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {
  value = '';
  genres = ['All', 'Fiction', 'Programming', 'Drama'];
  availableTags = ['Fantasy', 'Romance', 'Java', 'Python'];
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

  gridColumns = 6;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  currentData: Book[] = [];

  constructor() { }

  ngOnInit(): void {
    // get fullData first
    // inside getting data:
    this.totalSize = this.fullData.length;
    this.iterator();
  }
  handlePage(e: any): PageEvent {
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.iterator();
    return e;
  }

  iterator() {
    const end = (this.currentPage + 1) * this.pageSize;
    const start = this.currentPage * this.pageSize;
    const part = this.fullData.slice(start, end);
    this.currentData = part;
  }

  onTagRemoved(tag: string): void {
    const tags = this.tagsControl.value as string[];
    this.removeFirst(tags, tag);
    this.tagsControl.setValue(['']);
    this.tagsControl.setValue(tags);
    console.log(this.selectedTags);
  }

  private removeFirst<T>(array: T[], toRemove: T): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }


}
