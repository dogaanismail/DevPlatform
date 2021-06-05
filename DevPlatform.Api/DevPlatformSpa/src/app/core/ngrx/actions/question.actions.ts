import { Action } from '@ngrx/store';
import { Question } from '../../../models/question/question';

export enum QuestionActionTypes {
    ToggleQuestion = '[Question] Toggle Question',
    SetCurrentQuestion = '[Question] Set Current Question',
    ClearCurrentQuestion = '[Question] Clear Current Question',
    InitializeCurrentQuestion = '[Question] Initialize Current Question',
    Load = '[Question] Load',
    LoadSuccess = '[Question] Load Success',
    LoadFail = '[Question] Load Fail',
    UpdateQuestion = '[Question] Update Question',
    UpdateQuestionSuccess = '[Question] Update Question Success',
    UpdateQuestionFail = '[Question] Update Question Fail',
    CreateQuestion = '[Question] Create Question',
    CreateQuestionSuccess = '[Question] Create Question Success',
    CreateQuestionFail = '[Question] Create Question Fail',
    DeleteQuestion = '[Question] Delete Question',
    DeleteQuestionSuccess = '[Question] Delete Question Success',
    DeleteQuestionFail = '[Question] Delete Question Fail',
    CreateComment = '[Question] Create Comment',
    CreateCommentSuccess = '[Question] Create Comment Success',
    CreateCommentFail = '[Question] Create Comment Fail',
}

export class ToggleQuestion implements Action {
    readonly type = QuestionActionTypes.ToggleQuestion;

    constructor(public payload: boolean) { }
}

export class SetCurrentQuestion implements Action {
    readonly type = QuestionActionTypes.SetCurrentQuestion;

    constructor(public payload: number) { }
}

export class ClearCurrentQuestion implements Action {
    readonly type = QuestionActionTypes.ClearCurrentQuestion;
}

export class InitializeCurrentQuestion implements Action {
    readonly type = QuestionActionTypes.InitializeCurrentQuestion;
}

export class Load implements Action {
    readonly type = QuestionActionTypes.Load;
}

export class LoadSuccess implements Action {
    readonly type = QuestionActionTypes.LoadSuccess;

    constructor(public payload: Question[]) { }
}

export class LoadFail implements Action {
    readonly type = QuestionActionTypes.LoadFail;

    constructor(public payload: string) { }
}

export class CreateQuestion implements Action {
    readonly type = QuestionActionTypes.CreateQuestion;

    constructor(public payload: any) { }
}

export class CreateQuestionSuccess implements Action {
    readonly type = QuestionActionTypes.CreateQuestionSuccess;

    constructor(public payload: Question) { }
}

export class CreateQuestionFail implements Action {
    readonly type = QuestionActionTypes.CreateQuestionFail;

    constructor(public payload: string) { }
}

export class CreateComment implements Action {
    readonly type = QuestionActionTypes.CreateComment;

    constructor(public payload: any) { }
}

export class CreateCommentSuccess implements Action {
    readonly type = QuestionActionTypes.CreateCommentSuccess;

    constructor(public payload: any) { }
}

export class CreateCommentFail implements Action {
    readonly type = QuestionActionTypes.CreateCommentFail;

    constructor(public payload: string) { }
}

export type QuestionActions = ToggleQuestion
    | SetCurrentQuestion
    | ClearCurrentQuestion
    | InitializeCurrentQuestion
    | Load
    | LoadSuccess
    | LoadFail
    | CreateQuestion
    | CreateQuestionSuccess
    | CreateQuestionFail
    | CreateComment
    | CreateCommentSuccess
    | CreateCommentFail;
