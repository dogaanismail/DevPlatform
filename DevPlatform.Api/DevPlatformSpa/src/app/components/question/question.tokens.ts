import { InjectionToken } from '@angular/core';
import { StoreConfig } from '@ngrx/store/src/store_module';
import * as fromReducer from '../../core/ngrx/reducers/question.reducer';
import * as fromActions from '../../core/ngrx/actions/question.actions';

export const QUESTIONS_STORAGE_KEYS = new InjectionToken<keyof fromReducer.QuestionState[]>('QuestionsStorageKeys');
export const QUESTIONS_LOCAL_STORAGE_KEY = new InjectionToken<string[]>('QuestionsStorage');
export const QUESTIONS_CONFIG_TOKEN = new InjectionToken<StoreConfig<fromReducer.QuestionState, fromActions.QuestionActions>>('QuestionsConfigToken');
