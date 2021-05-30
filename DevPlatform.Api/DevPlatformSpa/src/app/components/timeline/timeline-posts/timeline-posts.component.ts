import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post/post';

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

  ngOnInit() {
  }

}
