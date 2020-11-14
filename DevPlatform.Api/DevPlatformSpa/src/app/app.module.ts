
/* Components */
import { AppComponent } from './app.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { NavbarComponent } from './components/shared/navbar/navbar-layout/navbar.component';

/* Modules */
import { QuestionModule } from './components/question/question.module';
import { ProfileModule } from './components/profile/profile.module';
import { TimelineModule } from './components/timeline/timeline.module';
import { ChatModule } from './components/clat/chat.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { StoryModule } from './components/storie/story.module';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TimelineModule,
    ProfileModule,
    QuestionModule,
    ChatModule,
    StoryModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
