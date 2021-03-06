import { FormatTitlePipe } from './core/pipes/format-title.pipe';
/* Components */
import { AppComponent } from './app.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { NavbarComponent } from './components/shared/navbar/navbar-layout/navbar.component';
import { SnowComponent } from './components/shared/snow/snow.component';

/* NgRx */
import { StoreModule, META_REDUCERS, MetaReducer, State, USER_PROVIDED_META_REDUCERS } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './core/ngrx/effects/user.effects';
import { UserAccountEffects } from './core/ngrx/effects/user-account.effects';
import { PostEffects } from './core/ngrx/effects/post.effects';
import { StoryEffects } from './core/ngrx/effects/story.effects';
import { reducers } from './core/ngrx/states/app.state';

/* Services */
import { AlertifyService } from './services/alertify/alertify.service';
import { AlbumService } from './services/album/album.service';

/* Modules */
import { QuestionModule } from './components/question/question.module';
import { ProfileModule } from './components/profile/profile.module';
import { TimelineModule } from './components/timeline/timeline.module';
import { ChatModule } from './components/clat/chat.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoryModule } from './components/storie/story.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DevPlatformMaterialModule } from './shared/modules/material.module';
import { NgxSpinnerModule } from "ngx-spinner";

/* Store Mechanism */
import { storageMetaReducer } from './core/store-infrastructure/storage-metareducer';
import { StoreLocalStorageService } from './core/store-infrastructure/store-local-storage.service';
import { ROOT_STORAGE_KEYS, ROOT_LOCAL_STORAGE_KEY } from './app.tokens';
import { environment } from 'src/environments/environment';
import { MultiStepFormComponent } from './components/authentication/multi-step-form/multi-step-form.component';


// factory meta-reducer configuration function
export function getMetaReducers(saveKeys: string[], localStorageKey: string, storageService: StoreLocalStorageService): MetaReducer<State<any>>[] {
  return [storageMetaReducer(saveKeys, localStorageKey, storageService)];
}


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    MultiStepFormComponent,
    FormatTitlePipe,
    SnowComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    DevPlatformMaterialModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    TimelineModule,
    ProfileModule,
    QuestionModule,
    ChatModule,
    StoryModule,
    StoreModule.forRoot(reducers),
    EffectsModule.forRoot([]),
    EffectsModule.forFeature(
      [UserEffects, UserAccountEffects, PostEffects, StoryEffects]
    ),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production })
  ],
  providers: [
    AlertifyService,
    AlbumService,
    { provide: ROOT_STORAGE_KEYS, useValue: ['users'], multi: true },
    { provide: ROOT_LOCAL_STORAGE_KEY, useValue: '__app_storage__', multi: true },
    {
      provide: USER_PROVIDED_META_REDUCERS,
      deps: [ROOT_STORAGE_KEYS, ROOT_LOCAL_STORAGE_KEY, StoreLocalStorageService],
      useFactory: getMetaReducers,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
