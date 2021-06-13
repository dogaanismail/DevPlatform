import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from '../../../../../models/user/signedUser';
import { Post } from '../../../../../models/post/post';
import { PostCommentCreate } from '../../../../../models/post/postCommentCreate';

/* NgRx */
import { Store } from '@ngrx/store';
import * as fromPost from '../../../../../core/ngrx/selectors/post.selectors';
import * as postActions from '../../../../../core/ngrx/actions/post.actions';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.scss']
})
export class CreateCommentComponent implements OnInit {

  constructor(private postStore: Store<fromPost.State>) { }

  @Input() signedUser: SignedUser;
  @Input() singlePost: any;
  commentCreate: PostCommentCreate = new PostCommentCreate;

  ngOnInit() {
  }

  saveComment() {
    this.commentCreate.postId = this.singlePost.id;
    this.postStore.dispatch(new postActions.CreateComment(this.commentCreate));
    this.commentCreate = new PostCommentCreate;
  }

}
