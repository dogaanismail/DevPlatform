import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { Post } from '../../../models/post/post';
import { SignedUser } from '../../../models/user/signedUser';
import { Story } from '../../../models/story/story';

@Component({
  selector: 'app-story-sidebar',
  templateUrl: './story-sidebar.component.html',
  styleUrls: ['./story-sidebar.component.scss']
})
export class StorySidebarComponent implements OnInit {

  @Input() signedUser: SignedUser;

  constructor() { }

  ngOnInit() {
  }

}
