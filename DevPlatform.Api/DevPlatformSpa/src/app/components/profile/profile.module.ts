import { ProfileCalendarComponent } from './profile-widgets/profile-calendar/profile-calendar.component';
import { ProfileVideosWidgetComponent } from './profile-widgets/profile-videos-widget/profile-videos-widget.component';
import { ProfileFriendsWidgetComponent } from './profile-widgets/profile-friends-widget/profile-friends-widget.component';
import { ProfilePhotosWidgetComponent } from './profile-widgets/profile-photos-widget/profile-photos-widget.component';
import { ProfileBasicInfoComponent } from './profile-widgets/profile-basic-info/profile-basic-info.component';
import { ProfilePostsComponent } from './common/profile-posts/profile-posts.component';
import { ProfileLayoutComponent } from './profile-layout/profile-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileAboutComponent } from './profile-about/profile-about.component';
import { ProfileFriendsComponent } from './profile-friends/profile-friends.component';
import { ProfilePhotosComponent } from './profile-photos/profile-photos.component';

const profileRoutes: Routes = [
    { path: "profile", component: ProfileLayoutComponent },
    { path: "profile/about", component: ProfileAboutComponent },
    { path: "profile/photos", component: ProfilePhotosComponent },
    { path: "profile/friends", component: ProfileFriendsComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(profileRoutes),
    ],
    declarations: [
        ProfileLayoutComponent,
        ProfileAboutComponent,
        ProfilePhotosComponent,
        ProfileFriendsComponent,
        ProfilePostsComponent,
        ProfileBasicInfoComponent,
        ProfilePhotosWidgetComponent,
        ProfileFriendsWidgetComponent,
        ProfileVideosWidgetComponent,
        ProfileCalendarComponent
    ]
})
export class ProfileModule { }
