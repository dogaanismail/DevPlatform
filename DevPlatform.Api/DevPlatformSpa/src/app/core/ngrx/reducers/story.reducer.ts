import { Story } from '../../../models/story/story';

import * as fromRoot from '../../ngrx/states/app.state';
import { StoryActions, StoryActionTypes } from '../actions/story.actions';

export interface State extends fromRoot.State {
    stories: StoryState;
}

export interface StoryState {
    showStoryId: boolean;
    currentStory: Story;
    currentStoryId: number;
    stories: Story[];
    commentStory: Story;
    isNewStory: boolean;
    error: string;
    isNewComment: boolean;
}

const initialState: StoryState = {
    showStoryId: true,
    currentStory: null,
    currentStoryId: null,
    stories: [],
    commentStory: null,
    isNewStory: false,
    error: '',
    isNewComment: false
};

export function storyReducer(state = initialState, action: StoryActions): StoryState {
    switch (action.type) {

        case StoryActionTypes.ToggleStory:
            return {
                ...state,
                showStoryId: action.payload
            };

        case StoryActionTypes.SetCurrentStory:
            return {
                ...state,
                currentStoryId: action.payload
            };

        case StoryActionTypes.LoadSuccess:
            return {
                ...state,
                stories: action.payload,
                error: ''
            };

        case StoryActionTypes.LoadFail:
            return {
                ...state,
                stories: [],
                error: action.payload
            };

        case StoryActionTypes.ClearCurrentStory:
            return {
                ...state,
                currentStoryId: null
            };

        case StoryActionTypes.InitializeCurrentStory:
            return {
                ...state,
                currentStoryId: 0
            };

        case StoryActionTypes.CreateStory:
            return {
                ...state,
                isNewStory: true
            };

        case StoryActionTypes.CreateStorySuccess:
            return {
                ...state,
                stories: [...state.stories, action.payload],
                error: '',
                isNewStory: false
            };

        case StoryActionTypes.CreateStoryFail:
            return {
                ...state,
                error: action.payload,
                isNewStory: false
            };

        case StoryActionTypes.CreateComment:
            return {
                ...state,
                isNewComment: true
            };

        case StoryActionTypes.CreateCommentSuccess:
            const story: Story = state.stories.filter((item: any) => item.id == action.payload.storyId)[0];
            story.comments.push(action.payload);
            return {
                ...state,
                stories: [...state.stories, action.payload],
                error: '',
                isNewComment: false
            };

        case StoryActionTypes.CreateCommentFail:
            return {
                ...state,
                error: action.payload,
                isNewComment: false
            };

        default:
            return state;
    }
}
