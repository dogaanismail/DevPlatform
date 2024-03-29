import { Injectable } from '@angular/core';
import { Observable, of, pipe } from 'rxjs';
import { mergeMap, map, catchError, switchMap, exhaustMap } from 'rxjs/operators';
import { QuestionService } from '../../../services/question/question.service';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, createEffect, Effect, ofType } from '@ngrx/effects';
import * as questionActions from '../actions/question.actions';
import { QuestionComment } from 'src/app/models/question/questionComment';

@Injectable()
export class QuestionEffects {
    constructor
        (
            private questionService: QuestionService,
            private actions$: Actions
        ) { }

    @Effect()
    loadQuestions$: Observable<Action> = this.actions$.pipe(
        ofType(questionActions.QuestionActionTypes.Load),
        mergeMap(action =>
            this.questionService.getQuestions().pipe(
                map((questions: any) => (new questionActions.LoadSuccess(questions.result))),
                catchError(err => of(new questionActions.LoadFail(err)))
            )
        )
    );

    @Effect()
    loadQuestionById$: Observable<Action> = this.actions$.pipe(
        ofType(questionActions.QuestionActionTypes.LoadById),
        map((action: questionActions.LoadById) => action.payload),
        mergeMap((questionId: number) =>
            this.questionService.getQuestionById(questionId).pipe(
                map((question: any) => (new questionActions.LoadByIdSuccess(question.result))),
                catchError(err => of(new questionActions.LoadByIdSuccess(err)))
            )
        )
    );

    @Effect()
    createQuestion$: Observable<Action> = this.actions$.pipe(
        ofType(questionActions.QuestionActionTypes.CreateQuestion),
        map(((action: questionActions.CreateQuestion) => action.payload)),
        mergeMap((question: any) =>
            this.questionService.createQuestion(question).pipe(
                map((res: any) => res.status ? new questionActions.CreateQuestionSuccess(res.result) : new questionActions.CreateQuestionFail(res.result.message)),
                catchError(err => of(new questionActions.CreateQuestionFail(err)))
            )
        ));


    @Effect()
    createComment$: Observable<Action> = this.actions$.pipe(
        ofType(questionActions.QuestionActionTypes.CreateComment),
        map((action: questionActions.CreateComment) => action.payload),
        mergeMap((comment: any) =>
            this.questionService.createComment(comment).pipe(
                map((newComment: any) => newComment.status ? new questionActions.CreateCommentSuccess(newComment.result as QuestionComment) :
                    new questionActions.CreateCommentFail(newComment.result.message)),
                catchError(err => of(new questionActions.CreateCommentFail(err)))
            )
        )
    );
}
