import { Component, Input, OnInit } from '@angular/core';

import { SignedUser } from 'src/app/models/user/signedUser';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @Input() signedUser: SignedUser;
  constructor() { }

  ngOnInit() {
  }

}
