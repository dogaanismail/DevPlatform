import { Injectable } from '@angular/core';
import { Observable, of, pipe } from 'rxjs';
import { mergeMap, map, catchError, switchMap } from 'rxjs/operators';
import { StoryService } from '../../../services/story/story.service';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import * as storyActions from '../actions/story.actions';


@Injectable()
export class StoryEffects {
    constructor
        (
            private storyService: StoryService,
            private actions$: Actions
        ) { }

    @Effect()
    loadStories$: Observable<Action> = this.actions$.pipe(
        ofType(storyActions.StoryActionTypes.Load),
        mergeMap(action =>
            this.storyService.getStories().pipe(
                map((stories: any) => (new storyActions.LoadSuccess(stories.result))),
                catchError(err => of(new storyActions.LoadFail(err)))
            )
        )
    );

    @Effect()
    createStory$: Observable<Action> = this.actions$.pipe(
        ofType(storyActions.StoryActionTypes.CreateStory),
        map(((action: storyActions.CreateStory) => action.payload)),
        switchMap((story: any) =>
            this.storyService.createStory(story).pipe(
                map((res: any) => res.status ? new storyActions.CreateStorySuccess(res.result) : new storyActions.CreateStoryFail(res.result.message)),
                catchError(err => of(new storyActions.CreateStoryFail(err)))
            )
        ));
}