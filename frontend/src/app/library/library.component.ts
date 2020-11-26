import { Component, OnInit } from '@angular/core';
import {FormControl} from '@angular/forms';

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
  constructor() { }

  ngOnInit(): void {
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
