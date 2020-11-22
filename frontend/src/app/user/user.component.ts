import { Component, OnInit } from '@angular/core';

interface Field{
  title:string;
  content:string;
  disabled:boolean;
}
interface History{
  contractID:number;
  bookName:string;
  startDate:string;
  endDate:string;
  branchName:string;
}

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})


export class UserComponent implements OnInit {
  fields:Field[] = [];
  displayedColumns = ['contractID', 'bookName', 'startDate', 'endDate', 'branchName'];
  dataSource:History[]= [];
  testFunction = () => {
    // field.disabled=!field.disabled
    console.log("lala")
  }
  
  constructor() { 
    var disabled:boolean=false;
  }

  ngOnInit(): void {
    
    this.fields = [
      {
        title:"Name",
        content:"kamal bassil",
        disabled:true,
      },
      {
        title:"User Name",
        content:"kamalbassil123",
        disabled:true,
      },
      {
        title:"Email",
        content:"kamal.bassil@lau.edu",
        disabled:true,
      },
      {
        title:"Phone Number",
        content:"71 123 456",
        disabled:true,
      },
      {
        title:"University ID",
        content:"LAU201800322",
        disabled:true,
      },
      {
        title:"UserID",
        content:"Idk if we want this if we have University ID",
        disabled:true,
      },

    ]
    //end of fields start of history
    this.dataSource=[
      {
        contractID:1,
        bookName:"War and Peace",
        startDate:"10/10/2020",
        endDate:"31/11/2020",
        branchName:"LAU Jbeil"
      },
      {
        contractID:2,
        bookName:"The Great Gatsby",
        startDate:"09/06/2020",
        endDate:"30/10/2020",
        branchName:"LAU Jbeil"
      },
      {
        contractID:1,
        bookName:"Lord of the Rings",
        startDate:"13/05/2020",
        endDate:"08/07/2020",
        branchName:"LAU Beirut"
      },
      {
        contractID:1,
        bookName:"War and Peace",
        startDate:"10/10/2020",
        endDate:"31/11/2020",
        branchName:"LAU Jbeil"
      },
      {
        contractID:2,
        bookName:"The Great Gatsby",
        startDate:"09/06/2020",
        endDate:"30/10/2020",
        branchName:"LAU Jbeil"
      },
      {
        contractID:1,
        bookName:"Lord of the Rings",
        startDate:"13/05/2020",
        endDate:"08/07/2020",
        branchName:"LAU Beirut"
      },
    ]

  }

}
