import { NavbarComponent } from './components/shared/navbar/navbar-layout/navbar.component';
import { TimelineModule } from './components/timeline/timeline.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TimelineModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
