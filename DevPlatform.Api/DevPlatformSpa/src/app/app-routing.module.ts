import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TimelineLayoutComponent } from './components/timeline/timeline-layout/timeline-layout.component';

const routes: Routes = [
  {
    path: '', component: TimelineLayoutComponent, pathMatch: 'full',
    loadChildren: () => import('./components/timeline/timeline.module').then(m => m.TimelineModule)
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
