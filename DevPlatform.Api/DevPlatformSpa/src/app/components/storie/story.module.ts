
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StoryFeedComponent } from './story-feed/story-feed.component';
import { StoryLayoutComponent } from './story-layout/story-layout.component';
import { StoryListComponent } from './story-list/story-list.component';
import { StorySidebarComponent } from './story-sidebar/story-sidebar.component';


import { storageMetaReducer } from '../../core/store-infrastructure/storage-metareducer';
import * as fromReducer from '../../core/ngrx/reducers/story.reducer';
import { StoreModule } from '@ngrx/store';
import { STORIES_CONFIG_TOKEN, STORIES_LOCAL_STORAGE_KEY, STORIES_STORAGE_KEYS } from './story.tokens';
import { StoreLocalStorageService } from '../../core/store-infrastructure/store-local-storage.service';
import { DevPlatformMaterialModule } from 'src/app/shared/modules/material.module';
import { SharedModule } from 'src/app/shared/modules/shared.module';

const storyRoutes: Routes = [
    { path: "story", component: StoryLayoutComponent }
    //TODO: must be question/detail/id?= 
];

export function getStoriesConfig(saveKeys: string[], localStorageKey: string, storageService: StoreLocalStorageService) {
    return { metaReducers: [storageMetaReducer(saveKeys, localStorageKey, storageService)] };
}

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(storyRoutes),
        StoreModule.forFeature('stories', fromReducer.storyReducer, STORIES_CONFIG_TOKEN),
        DevPlatformMaterialModule
    ],
    declarations: [
        StoryLayoutComponent,
        StorySidebarComponent,
        StoryListComponent,
        StoryFeedComponent
    ],
    providers: [
        StoreLocalStorageService,
        { provide: STORIES_LOCAL_STORAGE_KEY, useValue: '__stories_storage__' },
        { provide: STORIES_STORAGE_KEYS, useValue: ['stories', 'viewMode'] },
        {
            provide: STORIES_CONFIG_TOKEN,
            deps: [STORIES_STORAGE_KEYS, STORIES_LOCAL_STORAGE_KEY, StoreLocalStorageService],
            useFactory: getStoriesConfig
        },
    ]
})
export class StoryModule { }
