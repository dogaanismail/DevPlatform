import { Component, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from '../../../models/user/signedUser';
import { Story } from '../../../models/story/story';

/* Rxjs */
import { Observable } from 'rxjs';

/* NgRx */
import { Store, select } from '@ngrx/store';
import * as fromStory from '../../../core/ngrx/selectors/story.selectors';
import * as fromUser from '../../../core/ngrx/selectors/user.selectors';
import * as storyActions from '../../../core/ngrx/actions/story.actions';

@Component({
  selector: 'app-story-layout',
  templateUrl: './story-layout.component.html',
  styleUrls: ['./story-layout.component.css']
})

export class StoryLayoutComponent implements OnInit {

  constructor(
    private userStore: Store<fromUser.State>,
    private storyStore: Store<fromStory.State>) { }

  signedUser$: Observable<SignedUser>;
  stories$: Observable<Story[]>;

  ngOnInit() {
    this.storyStore.dispatch(new storyActions.Load());
    this.stories$ = this.storyStore.pipe(select(fromStory.getStories)) as Observable<Story[]>;
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
  }

}
