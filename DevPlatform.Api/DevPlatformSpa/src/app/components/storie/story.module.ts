
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StoryFeedComponent } from './story-feed/story-feed.component';
import { StoryLayoutComponent } from './story-layout/story-layout.component';
import { StoryListComponent } from './story-list/story-list.component';
import { StorySidebarComponent } from './story-sidebar/story-sidebar.component';

const storyRoutes: Routes = [
    { path: "story", component: StoryLayoutComponent }
    //TODO: must be question/detail/id?= 
];

@NgModule({
    imports: [
        RouterModule.forChild(storyRoutes),
    ],
    declarations: [
        StoryLayoutComponent,
        StorySidebarComponent,
        StoryListComponent,
        StoryFeedComponent
    ]
})
export class StoryModule { }
