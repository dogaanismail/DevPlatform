import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ModalService } from '../../../services/modal/modal.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Post } from 'src/app/models/post/post';

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

  constructor(
    private modalService: ModalService,
    private postStore: Store<fromPost.State>,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 1000);
    
    this.postStore.dispatch(new postActions.Load());
    this.posts$ = this.postStore.pipe(select(fromPost.getPosts)) as Observable<Post[]>;
    this.newPost$ = this.postStore.pipe(select(fromPost.getIsNewPost));
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
