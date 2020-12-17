import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
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
}

type tag = {
  tagName: string,
  bookISBN: string
}

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

  myBook: Book = {
    author: 'James Dashner',
    availableIn: [{name: 'Byblos Library', libId: 0}, {name: 'Beirut Library', libId: 1}],
    description: 'When Thomas wakes up in the lift, the only thing he can remember is his name. He’s surrounded by strangers—boys whose memories are also gone. Outside the towering stone walls that surround them is a limitless, ever-changing maze. It’s the only way out—and no one’s ever made it through alive. Then a girl arrives. The first girl ever. And the message she delivers is terrifying: Remember. Survive. Run.',
    name: 'The Maze Runner',
    genre: 'HELLO',
    tags: []
  };

  selectedId = 0;

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    const isbn = this.route.snapshot.paramMap.get('isbn');
    console.log(isbn);
    this.http.get<BookResponse>(`http://localhost:5000/book/byId?id=${isbn}`).subscribe(h => {
      console.log(h);

      this.myBook.author = h.author;
      this.myBook.description = 'I AM AN AMAZING DESCRIPTION';
      this.myBook.name = h.name;
      this.myBook.genre = h.genre;
      this.myBook.tags = h.tags.map(t => t.tagName);
      this.http.get(`http://localhost:5000/bookcopy/byId?id=${isbn}`).subscribe(h2 => {
          console.log(h2);
      });
    });
  }

}
