import { AgmCoreModule } from '@agm/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

import { AppRoutes } from './app.routes.service';
import { UserService } from './services/user/user.service'
import { QuoteService } from './services/quote/quote.service'


import { ViewsModule } from './views/views.module';
import { SharedModule } from './shared/shared.module';
import { ErrorModule } from './views/errors/error.module';

import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
// main layout
import { NavigationModule } from './main-layout/navigation/navigation.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        AgmCoreModule.forRoot({
            apiKey: ''
        }),
        BrowserModule,
        HttpModule,
        BrowserAnimationsModule,
        NavigationModule,
        HttpClientModule,
        AppRoutes,
        RouterModule,
        FormsModule,
        SharedModule,
        ViewsModule,
        ErrorModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    providers: [
        UserService,
        QuoteService
    ],
    bootstrap: [AppComponent],
    schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
