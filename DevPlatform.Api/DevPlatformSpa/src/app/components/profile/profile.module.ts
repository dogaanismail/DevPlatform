import { ProfilePhotosComponent } from './profile-photos/profile-photos.component';
import { ProfileLayoutComponent } from './profile-layout/profile-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileAboutComponent } from './profile-about/profile-about.component';
import { ProfileFriendsComponent } from './profile-friends/profile-friends.component';

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
        ProfileFriendsComponent
    ]
})
export class ProfileModule { }
