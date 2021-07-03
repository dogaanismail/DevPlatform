import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

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
  selector: 'app-question-detail',
  templateUrl: './question-detail.component.html',
  styleUrls: ['./question-detail.component.css']
})
export class QuestionDetailComponent implements OnInit {

  singleQuestion$: Observable<Question>;

  constructor(
    private questionStore: Store<fromQuestion.State>,
    private userStore: Store<fromUser.State>,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    const questionId = +this.route.snapshot.paramMap.get('id');
    if (questionId) {
      this.questionStore.dispatch(new questionActions.SetCurrentQuestion(questionId));
      this.singleQuestion$ = this.questionStore.pipe(select(fromQuestion.getCurrentQuestion)) as Observable<Question>;

      this.singleQuestion$.subscribe(question => {
        if (question == null) {
          this.questionStore.dispatch(new questionActions.LoadById(questionId));
          this.singleQuestion$ = this.questionStore.pipe(select(fromQuestion.getById)) as Observable<Question>;
        }
      })
    }
  }
}
