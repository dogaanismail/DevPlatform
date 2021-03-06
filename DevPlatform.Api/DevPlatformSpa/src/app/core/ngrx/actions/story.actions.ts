import { Action } from '@ngrx/store';
import { Story } from '../../../models/story/story';

export enum StoryActionTypes {
    ToggleStory = '[Story] Toggle Story',
    SetCurrentStory = '[Story] Set Current Story',
    ClearCurrentStory = '[Story] Clear Current Story',
    InitializeCurrentStory = '[Story] Initialize Current Story',
    Load = '[Story] Load',
    LoadSuccess = '[Story] Load Success',
    LoadFail = '[Story] Load Fail',
    UpdateStory = '[Story] Update Story',
    UpdateStorySuccess = '[Story] Update Story Success',
    UpdateStoryFail = '[Story] Update Story Fail',
    CreateStory = '[Story] Create Story',
    CreateStorySuccess = '[Story] Create Story Success',
    CreateStoryFail = '[Story] Create Story Fail',
    DeleteStory = '[Story] Delete Story',
    DeleteStorySuccess = '[Story] Delete Story Success',
    DeleteStoryFail = '[Story] Delete Story Fail',
    CreateComment = '[Story] Create Comment',
    CreateCommentSuccess = '[Story] Create Comment Success',
    CreateCommentFail = '[Story] Create Comment Fail'
}

export class ToggleStory implements Action {
    readonly type = StoryActionTypes.ToggleStory;

    constructor(public payload: boolean) { }
}

export class SetCurrentStory implements Action {
    readonly type = StoryActionTypes.SetCurrentStory;

    constructor(public payload: number) { }
}

export class ClearCurrentStory implements Action {
    readonly type = StoryActionTypes.ClearCurrentStory;
}

export class InitializeCurrentStory implements Action {
    readonly type = StoryActionTypes.InitializeCurrentStory;
}

export class Load implements Action {
    readonly type = StoryActionTypes.Load;
}

export class LoadSuccess implements Action {
    readonly type = StoryActionTypes.LoadSuccess;

    constructor(public payload: Story[]) { }
}

export class LoadFail implements Action {
    readonly type = StoryActionTypes.LoadFail;

    constructor(public payload: string) { }
}

export class CreateStory implements Action {
    readonly type = StoryActionTypes.CreateStory;

    constructor(public payload: any) { }
}

export class CreateStorySuccess implements Action {
    readonly type = StoryActionTypes.CreateStorySuccess;

    constructor(public payload: Story) { }
}

export class CreateStoryFail implements Action {
    readonly type = StoryActionTypes.CreateStoryFail;

    constructor(public payload: string) { }
}

export class CreateComment implements Action {
    readonly type = StoryActionTypes.CreateComment;

    constructor(public payload: any) { }
}

export class CreateCommentSuccess implements Action {
    readonly type = StoryActionTypes.CreateCommentSuccess;

    constructor(public payload: any) { }
}

export class CreateCommentFail implements Action {
    readonly type = StoryActionTypes.CreateCommentFail;

    constructor(public payload: string) { }
}


export type StoryActions = ToggleStory
    | SetCurrentStory
    | ClearCurrentStory
    | InitializeCurrentStory
    | Load
    | LoadSuccess
    | LoadFail
    | CreateStory
    | CreateStorySuccess
    | CreateStoryFail
    | CreateComment
    | CreateCommentSuccess
    | CreateCommentFail;
