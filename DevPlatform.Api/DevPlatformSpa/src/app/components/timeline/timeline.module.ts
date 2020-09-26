import { TimelineLayoutComponent } from './timeline-layout/timeline-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const timelineRoutes: Routes = [
    { path: "timeline", component: TimelineLayoutComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(timelineRoutes),
    ],
    declarations: [
        TimelineLayoutComponent
    ]
})
export class TimelineModule { }
