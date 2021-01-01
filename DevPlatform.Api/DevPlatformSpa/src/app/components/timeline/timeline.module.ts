import { TimelineActivityComponent } from './timeline-widgets/timeline-activity/timeline-activity.component';
import { TimelineWeatherComponent } from './timeline-widgets/timeline-weather/timeline-weather.component';
import { TimelineBirthdayComponent } from './timeline-widgets/timeline-birthday/timeline-birthday.component';
import { TimelineSuggestFriendsComponent } from './timeline-widgets/timeline-suggest-friends/timeline-suggest-friends.component';
import { TimelineStoriesComponent } from './timeline-widgets/timeline-stories/timeline-stories.component';
import { TimelinePostsComponent } from './timeline-posts/timeline-posts.component';
import { TimelineLayoutComponent } from './timeline-layout/timeline-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TimelineCreatePostComponent } from './timeline-create-post/timeline-create-post.component';
import { TimelineNewjobComponent } from './timeline-widgets/timeline-newjob/timeline-newjob.component';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { CreateAlbumModalComponent } from './modals/create-album-modal/create-album-modal.component';
import { DropzoneModule } from 'ngx-dropzone-wrapper';


const timelineRoutes: Routes = [
    { path: "timeline", component: TimelineLayoutComponent }
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(timelineRoutes),
        DropzoneModule
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

    ]
})
export class TimelineModule { }
