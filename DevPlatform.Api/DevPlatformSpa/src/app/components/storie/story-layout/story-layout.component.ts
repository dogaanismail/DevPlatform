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
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  selector: 'app-story-layout',
  templateUrl: './story-layout.component.html',
  styleUrls: ['./story-layout.component.css']
})

export class StoryLayoutComponent implements OnInit {

  constructor(
    private userStore: Store<fromUser.State>,
    private storyStore: Store<fromStory.State>,
    private modalService: ModalService) { }

  signedUser$: Observable<SignedUser>;
  stories$: Observable<Story[]>;
  newStory$: Observable<boolean>;
  storyErrorMessage$: Observable<string>;

  ngOnInit() {
    this.storyStore.dispatch(new storyActions.Load());
    this.stories$ = this.storyStore.pipe(select(fromStory.getStories)) as Observable<Story[]>;
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
    this.newStory$ = this.storyStore.pipe(select(fromStory.getIsNewStory));
    this.storyErrorMessage$ = this.storyStore.pipe(select(fromStory.getError));
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
