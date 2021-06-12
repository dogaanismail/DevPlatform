import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from '../../../../../models/user/signedUser';
import { Post } from '../../../../../models/post/post';

@Component({
  selector: 'app-comment-layout',
  templateUrl: './comment-layout.component.html',
  styleUrls: ['./comment-layout.component.css']
})
export class CommentLayoutComponent implements OnInit {

  constructor() { }

  @Input() signedUser: SignedUser;
  @Input() singlePost: Post;

  ngOnInit() {
  }

}
