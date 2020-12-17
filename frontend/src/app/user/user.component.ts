import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

interface Field{
  title: string;
  content: string;
  disabled: boolean;
  editable: boolean;
}
interface History{
  bookName: string, position: number
}

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})


export class UserComponent implements OnInit {
  displayedColumns = ['contractID', 'bookName', 'startDate', 'endDate', 'branchName'];
  displayedColumnsQ = ['bookName', 'queueNumber', 'estimatedTime'];
  dataSource: History[] = [];
  testFunction = () => {
    // field.disabled=!field.disabled
    console.log('lala');
  }

  constructor(private http: HttpClient, private route: Router) {
  }

  ngOnInit(): void {
    // end of fields start of history
    this.http.get<{bookName: string, position: number}[]>('http://localhost:5000/Waitlist', {withCredentials: true}).subscribe(h => {
      this.dataSource = h;
    });
  }

}
