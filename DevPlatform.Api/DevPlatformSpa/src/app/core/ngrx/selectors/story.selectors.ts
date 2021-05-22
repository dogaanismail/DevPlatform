import * as fromRoot from '../states/app.state';
import * as fromStories from '../reducers/story.reducer';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export interface State extends fromRoot.State {
    stories: fromStories.StoryState;
}

const getStoryFeatureState = createFeatureSelector<fromStories.StoryState>('stories');

export const getShowStoryId = createSelector(
    getStoryFeatureState,
    state => state.showStoryId
);

export const getCurrentStoryId = createSelector(
    getStoryFeatureState,
    state => state.currentStoryId
);

export const getStories = createSelector(
    getStoryFeatureState,
    state => state.stories
);

export const getIsNewStory = createSelector(
    getStoryFeatureState,
    state => state.isNewStory
);

export const getIsNewComment = createSelector(
    getStoryFeatureState,
    state => state.isNewComment
);

export const getCurrentStory = createSelector(
    getStoryFeatureState,
    getCurrentStoryId,
    (state, currentStoryId) => {
        if (currentStoryId === 0) {
            return {
                Id: 0,
                text: "",
                createdByUserName: "",
                createdByUserPhoto: "",
                imageUrl: "",
                videoUrl: "",
                createdDate: null,
                storyType: null,
                comments: null
            };
        } else {
            return currentStoryId ? state.stories.find((st: any) => st.id == currentStoryId) : null;
        }
    }
);

export const getError = createSelector(
    getStoryFeatureState,
    state => state.error
);