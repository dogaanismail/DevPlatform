import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-snow',
  templateUrl: './snow.component.html',
  styleUrls: ['./snow.component.scss']
})
export class SnowComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  arrayOne(n: number): any[] {
    return Array(n);
  }

}
