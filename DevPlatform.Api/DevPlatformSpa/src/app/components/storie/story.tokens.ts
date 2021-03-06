import { InjectionToken } from '@angular/core';
import { StoreConfig } from '@ngrx/store/src/store_module';
import * as fromReducer from '../../core/ngrx/reducers/story.reducer';
import * as fromActions from '../../core/ngrx/actions/story.actions';

export const STORIES_STORAGE_KEYS = new InjectionToken<keyof fromReducer.StoryState[]>('StoriesStorageKeys');
export const STORIES_LOCAL_STORAGE_KEY = new InjectionToken<string[]>('StoriesStorage');
export const STORIES_CONFIG_TOKEN = new InjectionToken<StoreConfig<fromReducer.StoryState, fromActions.StoryActions>>('StoriesConfigToken');
