import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';

interface Library{
  name: string;
  libId: number;
}


interface Book {
  name: string;
  author: string;
  description: string;
  availableIn: Library[];
  genre: string;
  tags: string[];
  isbn: string;
}

type tag = {
  tagName: string,
  bookISBN: string
};

type BookResponse = {
  author: string,
  genre: string,
  isbn: string,
  name: string,
  publisher: string,
  releaseDate: string,
  tags: tag[]
};

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  bookCopies: {copyId: number, branchId: number}[] = [];
  myBook: Book = {
    author: 'James Dashner',
    isbn: '',
    availableIn: [{name: 'Byblos Library', libId: 1}, {name: 'Beirut Library', libId: 2}],
    description: 'When Thomas wakes up in the lift, the only thing he can remember is his name. He’s surrounded by strangers—boys whose memories are also gone. Outside the towering stone walls that surround them is a limitless, ever-changing maze. It’s the only way out—and no one’s ever made it through alive. Then a girl arrives. The first girl ever. And the message she delivers is terrifying: Remember. Survive. Run.',
    name: 'The Maze Runner',
    genre: 'HELLO',
    tags: []
  };

  addWaitlist = false;
  selectedId = 0;

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

  onAddCart(): void {
    if (this.selectedId !== 0) {
      let foundId = -1;
      this.bookCopies.forEach(x => {
        console.log(x);
        if (x.branchId === this.selectedId)
        {
          foundId = x.copyId;
        }
      });
      this.http.get(`http://localhost:5000/Cart/add?copyId=${foundId}`, {withCredentials: true}).subscribe(h => {
        this.router.navigate(['/cart']);
      });
    }
  }

  onAddWaitlist(): void {
    this.http.get(`http://localhost:5000/Cart/towaitlist?ISBN=${this.myBook.isbn}`, {withCredentials: true}).subscribe(h => {
      this.router.navigate(['/']);
    });
  }

  ngOnInit(): void {
    const isbn = this.route.snapshot.paramMap.get('isbn');
    console.log(isbn);
    this.http.get<BookResponse>(`http://localhost:5000/book/byId?id=${isbn}`).subscribe(h => {
      console.log(h);

      this.myBook.author = h.author;
      this.myBook.description = 'I AM AN AMAZING DESCRIPTION';
      this.myBook.name = h.name;
      this.myBook.genre = h.genre;
      this.myBook.isbn = h.isbn;
      this.myBook.tags = h.tags.map(t => t.tagName);
      this.http.get<{copyId: number, branchId: number}[]>(`http://localhost:5000/bookcopy/byId?id=${isbn}`).subscribe(h2 => {
          console.log(h2);
          if (h2.length === 0) {
            this.addWaitlist = true;
            this.myBook.availableIn = [];
          } else {
            this.addWaitlist = false;
            const libIds: number[] = [];
            const libraries: { name: string, libId: number }[] = [];
            h2.forEach((l, i) => {
              if (!libIds.includes(l.branchId)) {
                if (l.branchId === 1) {
                  libraries.push({name: 'Byblos Library', libId: 1});
                } else {
                  libraries.push({name: 'Beirut Library', libId: 2});
                }
                libIds.push(l.branchId);
              }
            });
            this.myBook.availableIn = libraries;
            this.bookCopies = h2;
          }
      });
    });
  }

}
