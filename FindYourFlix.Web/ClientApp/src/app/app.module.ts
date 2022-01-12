import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MoviesListComponent} from "./movies/movies-list/movies-list.component";
import {MoviesItemComponent} from "./movies/movie-item/movies-item.component";
import { RouterModule} from "@angular/router";
import {MoviesService} from "./services/movies.service";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {TagsService} from "./services/tags.service";
import {UsersService} from "./services/users.service";
import {FaIconLibrary, FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import { faStar as farStar } from '@fortawesome/free-regular-svg-icons';
import { faStar as fasStar } from '@fortawesome/free-solid-svg-icons';
import {AppHeaderComponent} from "./app-header/app-header.component";
import {SearchBarComponent} from "./search-bar/search-bar.component";
import {RegisterComponent} from "./register/register.component";
import {ProfileComponent} from "./profile/profile.component";
import {CommonModule} from "@angular/common";
import {AuthenticationService} from "./services/authentication.service";
import {LoginComponent} from "./login/login.component";
import {AuthorizationCheck} from "./services/authorizationCheck";
import {httpInterceptor} from "./services/httpInterceptor";
import {ErrorInterceptor} from "./services/errorInterceptor";
import {PerfectScrollbarModule} from "ngx-perfect-scrollbar";

@NgModule({
  declarations: [
    AppComponent,
    MoviesListComponent,
    MoviesItemComponent,
    AppHeaderComponent,
    SearchBarComponent,
    RegisterComponent,
    ProfileComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot([
      {path: 'profile', component: ProfileComponent, canActivate: [AuthorizationCheck]},
      {path: 'movies', component: MoviesListComponent, canActivate: [AuthorizationCheck]},
      {path: 'register', component: RegisterComponent},
      {path: 'login', component: LoginComponent},
    ]),
    FormsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    PerfectScrollbarModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
    // { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    MoviesService,
    BsModalService,
    TagsService,
    UsersService,
    BsModalRef,
    FormBuilder,
    AuthenticationService,
    AuthorizationCheck
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(fasStar, farStar);
  }
}
