import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { ProfileLayoutComponent } from './components/profile/profile-layout/profile-layout.component';
import { QuestionLayoutComponent } from './components/question/question-layout/question-layout.component';
import { TimelineLayoutComponent } from './components/timeline/timeline-layout/timeline-layout.component';

const routes: Routes = [
  {
    path: '', component: TimelineLayoutComponent, pathMatch: 'full',
    loadChildren: () => import('./components/timeline/timeline.module').then(m => m.TimelineModule)
  },
  {
    path: "profile", component: ProfileLayoutComponent,  //TODO: must be profile/:username
    loadChildren: () => import('./components/profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: "question", component: QuestionLayoutComponent,
    loadChildren: () => import('./components/question/question.module').then(m => m.QuestionModule)
  },
  { path: "account/login", component: LoginComponent },
  { path: "account/register", component: RegisterComponent }

  //TODO: Import the other modules here
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
