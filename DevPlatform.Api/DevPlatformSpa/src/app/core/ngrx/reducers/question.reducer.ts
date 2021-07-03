import { Question } from '../../../models/question/question';

import * as fromRoot from '../../ngrx/states/app.state';
import { QuestionActions, QuestionActionTypes } from '../actions/question.actions';

export interface State extends fromRoot.State {
    questions: QuestionState;
}

export interface QuestionState {
    showQuestionId: boolean;
    currentQuestion: Question;
    currentQuestionId: number;
    questions: Question[];
    commentQuestion: Question;
    isNewQuestion: boolean;
    error: string;
    isNewComment: boolean;
}

const initialState: QuestionState = {
    showQuestionId: true,
    currentQuestion: null,
    currentQuestionId: null,
    questions: [],
    commentQuestion: null,
    isNewQuestion: false,
    error: '',
    isNewComment: false
};

export function questionReducer(state = initialState, action: QuestionActions): QuestionState {
    switch (action.type) {

        case QuestionActionTypes.ToggleQuestion:
            return {
                ...state,
                showQuestionId: action.payload
            };

        case QuestionActionTypes.SetCurrentQuestion:
            return {
                ...state,
                currentQuestionId: action.payload
            };

        case QuestionActionTypes.LoadSuccess:
            return {
                ...state,
                questions: action.payload,
                error: ''
            };

        case QuestionActionTypes.LoadFail:
            return {
                ...state,
                questions: [],
                error: action.payload
            };

        case QuestionActionTypes.LoadByIdSuccess:
            return {
                ...state,
                currentQuestion: action.payload,
                error: ''
            };

        case QuestionActionTypes.LoadByIdFail:
            return {
                ...state,
                currentQuestion: null,
                error: action.payload
            };

        case QuestionActionTypes.ClearCurrentQuestion:
            return {
                ...state,
                currentQuestionId: null
            };

        case QuestionActionTypes.InitializeCurrentQuestion:
            return {
                ...state,
                currentQuestionId: 0
            };

        case QuestionActionTypes.CreateQuestion:
            return {
                ...state,
                isNewQuestion: true
            };

        case QuestionActionTypes.CreateQuestionSuccess:
            return {
                ...state,
                questions: [...state.questions, action.payload].sort((a, b) => <any>new Date(b.createdDate) - <any>new Date(a.createdDate)),
                error: '',
                isNewQuestion: false
            };

        case QuestionActionTypes.CreateQuestionFail:
            return {
                ...state,
                error: action.payload,
                isNewQuestion: false
            };

        case QuestionActionTypes.CreateComment:
            return {
                ...state,
                isNewComment: true
            };

        case QuestionActionTypes.CreateCommentSuccess:
            const question: Question = state.questions.filter((item: any) => item.id == action.payload.questionId)[0];
            question.comments.push(action.payload);
            return {
                ...state,
                questions: [...state.questions, action.payload],
                error: '',
                isNewComment: false
            };

        case QuestionActionTypes.CreateCommentFail:
            return {
                ...state,
                error: action.payload,
                isNewComment: false
            };

        default:
            return state;
    }
}
