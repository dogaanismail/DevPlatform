import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { Post } from '../../../models/post/post';
import { SignedUser } from '../../../models/user/signedUser';

/* NgRx */
import { Store } from '@ngrx/store';
import * as fromPost from '../../../core/ngrx/selectors/post.selectors';
import * as postActions from '../../../core/ngrx/actions/post.actions';

@Component({
  selector: 'app-timeline-posts',
  templateUrl: './timeline-posts.component.html',
  styleUrls: ['./timeline-posts.component.scss']
})
export class TimelinePostsComponent implements OnInit {

  constructor(
    private postStore: Store<fromPost.State>,
  ) { }

  pageTitle = 'Timeline';
  @Input() posts: Post[];
  @Input() newPost: boolean;
  @Input() newComment: boolean;
  @Input() signedUser: SignedUser;

  ngOnInit() {
  }

}
