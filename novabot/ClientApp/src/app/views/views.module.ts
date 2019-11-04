import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgmCoreModule } from '@agm/core';

import { CalendarModule, } from 'angular-calendar';
import { SharedModule } from '../shared/shared.module';

import { QuoteRowComponent } from './quote-row/quote-row.component';
import { UserCardComponent } from './user-card/user-card.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { QuoteListComponent } from './quotes-list/quotes-list.component';
import { MainPageComponent } from '../main-layout/main-page/main-page.component';
import { UsersPageComponent } from '../main-layout/users-page/users-page.component';






@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        BrowserModule,
        BrowserAnimationsModule,
        SharedModule,
        AgmCoreModule.forRoot({
            // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en#key
            apiKey: ''
        }),
        CalendarModule.forRoot()
    ],
    declarations: [
        UserListComponent,
        UserCardComponent,
        UserProfileComponent,
        QuoteRowComponent,
        QuoteListComponent,
        MainPageComponent,
        UsersPageComponent
        
    ],
    exports: [
        QuoteRowComponent,
        UserListComponent,
        UserProfileComponent
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class ViewsModule { }
