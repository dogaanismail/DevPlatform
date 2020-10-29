import { ProfileLayoutComponent } from './profile-layout/profile-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const profileRoutes: Routes = [
    { path: "profile", component: ProfileLayoutComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(profileRoutes),
    ],
    declarations: [
        ProfileLayoutComponent
    ]
})
export class ProfileModule { }
