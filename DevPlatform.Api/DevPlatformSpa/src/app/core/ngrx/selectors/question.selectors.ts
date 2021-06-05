import * as fromRoot from '../states/app.state';
import * as fromQuestions from '../reducers/question.reducer';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export interface State extends fromRoot.State {
    questions: fromQuestions.QuestionState;
}

const getQuestionsFeatureState = createFeatureSelector<fromQuestions.QuestionState>('questions');

export const getShowQuestionId = createSelector(
    getQuestionsFeatureState,
    state => state.showQuestionId
);

export const getCurrentQuestionId = createSelector(
    getQuestionsFeatureState,
    state => state.currentQuestionId
);

export const getQuestions = createSelector(
    getQuestionsFeatureState,
    state => state.questions
);

export const getIsNewQuestion = createSelector(
    getQuestionsFeatureState,
    state => state.isNewQuestion
);

export const getIsNewComment = createSelector(
    getQuestionsFeatureState,
    state => state.isNewComment
);

export const getCurrentQuestion = createSelector(
    getQuestionsFeatureState,
    getCurrentQuestionId,
    (state, currentQuestionId) => {
        if (currentQuestionId === 0) {
            return {
                Id: 0,
                text: "",
                description: "",
                createdByUserName: "",
                createdByUserPhoto: "",
                createdDate: null,
                comments: null
            };
        } else {
            return currentQuestionId ? state.questions.find((p: any) => p.id == currentQuestionId) : null;
        }
    }
);

export const getError = createSelector(
    getQuestionsFeatureState,
    state => state.error
);