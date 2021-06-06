import { Component, OnInit } from '@angular/core';

/* Models */
import { Question } from '../../../models/question/question';
import { SignedUser } from '../../../models/user/signedUser';

/* Rxjs */
import { Observable } from 'rxjs';

/* NgRx */
import { Store, select } from '@ngrx/store';
import * as fromQuestion from '../../../core/ngrx/selectors/question.selectors';
import * as fromUser from '../../../core/ngrx/selectors/user.selectors';
import * as questionActions from '../../../core/ngrx/actions/question.actions';


@Component({
  selector: 'app-question-layout',
  templateUrl: './question-layout.component.html',
  styleUrls: ['./question-layout.component.scss']
})
export class QuestionLayoutComponent implements OnInit {

  componentActive = true;
  questions$: Observable<Question[]>;
  newQuestion$: Observable<boolean>;
  errorMessage$: Observable<string>;
  signedUser$: Observable<SignedUser>;

  constructor(
    private questionStore: Store<fromQuestion.State>,
    private userStore: Store<fromUser.State>
  ) { }

  ngOnInit() {
    this.questionStore.dispatch(new questionActions.Load());
    this.questions$ = this.questionStore.pipe(select(fromQuestion.getQuestions)) as Observable<Question[]>;
    this.newQuestion$ = this.questionStore.pipe(select(fromQuestion.getIsNewQuestion));
    this.errorMessage$ = this.questionStore.pipe(select(fromQuestion.getError));
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
  }

}
