import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ModalService } from '../../../services/modal/modal.service';
import { NgxSpinnerService } from 'ngx-spinner';

/* Models */
import { Post } from '../../../models/post/post';
import { SignedUser } from '../../../models/user/signedUser';

/* Rxjs */
import { Observable } from 'rxjs';
/* NgRx */
import { Store, select } from '@ngrx/store';
import * as fromPost from '../../../core/ngrx/selectors/post.selectors';
import * as fromStory from '../../../core/ngrx/selectors/story.selectors';
import * as fromUser from '../../../core/ngrx/selectors/user.selectors';
import * as postActions from '../../../core/ngrx/actions/post.actions';
import * as storyActions from '../../../core/ngrx/actions/story.actions';
import { Story } from 'src/app/models/story/story';

@Component({
  selector: 'app-timeline-layout',
  templateUrl: './timeline-layout.component.html',
  styleUrls: ['./timeline-layout.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimelineLayoutComponent implements OnInit {

  componentActive = true;
  posts$: Observable<Post[]>;
  stories$: Observable<Story[]>;
  newPost$: Observable<boolean>;
  newStory$: Observable<boolean>;
  errorMessage$: Observable<string>;
  storyErrorMessage$: Observable<string>;
  signedUser$: Observable<SignedUser>;

  constructor(
    private modalService: ModalService,
    private postStore: Store<fromPost.State>,
    private userStore: Store<fromUser.State>,
    private storyStore: Store<fromStory.State>,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.postStore.dispatch(new postActions.Load());
    this.posts$ = this.postStore.pipe(select(fromPost.getPosts)) as Observable<Post[]>;
    this.stories$ = this.postStore.pipe(select(fromStory.getStories)) as Observable<Story[]>;
    this.newPost$ = this.postStore.pipe(select(fromPost.getIsNewPost));
    this.newStory$ = this.postStore.pipe(select(fromStory.getIsNewStory));
    this.errorMessage$ = this.postStore.pipe(select(fromPost.getError));
    this.storyErrorMessage$ = this.storyStore.pipe(select(fromStory.getError));
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
