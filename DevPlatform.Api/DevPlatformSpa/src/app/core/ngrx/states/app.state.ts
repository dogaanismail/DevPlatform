import { ActionReducerMap } from '@ngrx/store';
import * as fromUser from '../../ngrx/reducers/user.reducer';
import * as fromStory from '../../ngrx/reducers/story.reducer';

export interface State {
    users: fromUser.UserState;
    stories: fromStory.StoryState
}

export const reducers: ActionReducerMap<State> = {
    users: fromUser.userReducer,
    stories: fromStory.storyReducer
};