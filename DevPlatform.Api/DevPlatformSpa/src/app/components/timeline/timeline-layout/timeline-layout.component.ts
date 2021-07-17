import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

/* Services */
import { ModalService } from '../../../services/modal/modal.service';
import { WeatherService } from 'src/app/services/common/weather/Weather.service';

/* Models */
import { Post } from '../../../models/post/post';
import { SignedUser } from '../../../models/user/signedUser';
import { WeatherResponse } from '../../../models/common/weatherResponse';

/* Rxjs */
import { Observable } from 'rxjs';

/* NgRx */
import { Store, select } from '@ngrx/store';
import * as fromPost from '../../../core/ngrx/selectors/post.selectors';
import * as fromUser from '../../../core/ngrx/selectors/user.selectors';
import * as postActions from '../../../core/ngrx/actions/post.actions';

@Component({
  selector: 'app-timeline-layout',
  templateUrl: './timeline-layout.component.html',
  styleUrls: ['./timeline-layout.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimelineLayoutComponent implements OnInit {

  componentActive = true;
  posts$: Observable<Post[]>;
  newPost$: Observable<boolean>;
  errorMessage$: Observable<string>;
  signedUser$: Observable<SignedUser>;
  newComment$: Observable<boolean>;
  currentWeather: WeatherResponse;

  constructor(
    private modalService: ModalService,
    private postStore: Store<fromPost.State>,
    private userStore: Store<fromUser.State>,
    private weatherService: WeatherService) { }

  ngOnInit() {
    this.postStore.dispatch(new postActions.Load());
    this.posts$ = this.postStore.pipe(select(fromPost.getPosts)) as Observable<Post[]>;
    this.newPost$ = this.postStore.pipe(select(fromPost.getIsNewPost));
    this.errorMessage$ = this.postStore.pipe(select(fromPost.getError));
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
    this.newComment$ = this.postStore.pipe(select(fromPost.getIsNewComment));
    this.weatherService.getCurrentWeather().subscribe((response: any) => {
      this.currentWeather = response.result;
    })
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
