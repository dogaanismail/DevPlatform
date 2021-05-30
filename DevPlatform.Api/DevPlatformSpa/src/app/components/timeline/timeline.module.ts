/*Components */
import { TimelineActivityComponent } from './timeline-widgets/timeline-activity/timeline-activity.component';
import { TimelineWeatherComponent } from './timeline-widgets/timeline-weather/timeline-weather.component';
import { TimelineBirthdayComponent } from './timeline-widgets/timeline-birthday/timeline-birthday.component';
import { TimelineSuggestFriendsComponent } from './timeline-widgets/timeline-suggest-friends/timeline-suggest-friends.component';
import { TimelineStoriesComponent } from './timeline-widgets/timeline-stories/timeline-stories.component';
import { TimelinePostsComponent } from './timeline-posts/timeline-posts.component';
import { TimelineLayoutComponent } from './timeline-layout/timeline-layout.component';
import { TimelineCreatePostComponent } from './timeline-create-post/timeline-create-post.component';
import { TimelineNewjobComponent } from './timeline-widgets/timeline-newjob/timeline-newjob.component';
import { CreateAlbumModalComponent } from './modals/create-album-modal/create-album-modal.component';

/*Modules*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DevPlatformMaterialModule } from '../../shared/modules/material.module';
import { NgxGalleryModule } from 'ngx-gallery-9';

/*Ngrx and Store infrastructure implementations */
import { storageMetaReducer } from '../../core/store-infrastructure/storage-metareducer';
import * as fromReducer from '../../core/ngrx/reducers/post.reducer';
import { StoreModule } from '@ngrx/store';
import { POSTS_CONFIG_TOKEN, POSTS_LOCAL_STORAGE_KEY, POSTS_STORAGE_KEYS } from './timeline.tokens';
import { StoreLocalStorageService } from '../../core/store-infrastructure/store-local-storage.service';

const timelineRoutes: Routes = [
    { path: "timeline", component: TimelineLayoutComponent }
];

export function getPostsConfig(saveKeys: string[], localStorageKey: string, storageService: StoreLocalStorageService) {
    return { metaReducers: [storageMetaReducer(saveKeys, localStorageKey, storageService)] };
}

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(timelineRoutes),
        DropzoneModule,
        NgxGalleryModule,
        StoreModule.forFeature('posts', fromReducer.postReducer, POSTS_CONFIG_TOKEN),
        DevPlatformMaterialModule
    ],
    declarations: [
        TimelineLayoutComponent,
        TimelinePostsComponent,
        TimelineCreatePostComponent,
        TimelineStoriesComponent,
        TimelineSuggestFriendsComponent,
        TimelineNewjobComponent,
        TimelineBirthdayComponent,
        TimelineWeatherComponent,
        TimelineActivityComponent,
        CreateAlbumModalComponent
    ],
    providers: [
        StoreLocalStorageService,
        { provide: POSTS_LOCAL_STORAGE_KEY, useValue: '__posts_storage__' },
        { provide: POSTS_STORAGE_KEYS, useValue: ['posts', 'viewMode'] },
        {
            provide: POSTS_CONFIG_TOKEN,
            deps: [POSTS_STORAGE_KEYS, POSTS_LOCAL_STORAGE_KEY, StoreLocalStorageService],
            useFactory: getPostsConfig
        },
    ]

})
export class TimelineModule { }
