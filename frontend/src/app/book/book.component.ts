import { Component, OnInit } from '@angular/core';

interface Library{
  name: string;
  libId: number;
}


interface Book {
  name: string;
  author: string;
  description: string;
  availableIn: Library[];
}

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
    name: 'The Maze Runner'
  };

  selectedId = 0;

  constructor() { }

  ngOnInit(): void {
  }

}
