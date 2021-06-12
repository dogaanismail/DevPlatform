import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { PostComment } from '../../../../../models/post/postComment';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {

  constructor() { }

  @Input() commentList: PostComment[];

  ngOnInit() {
  }

}
