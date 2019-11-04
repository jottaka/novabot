import { RouterModule, Route } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { NotFoundComponent } from './views/errors/not-found/not-found.component';
import { UserProfileComponent } from './views/user-profile/user-profile.component';
import { MainPageComponent } from './main-layout/main-page/main-page.component';
import { UsersPageComponent } from './main-layout/users-page/users-page.component';




const routes: Route[] = [
    { path: '', pathMatch: 'full', redirectTo: 'quotes' },

    { path: 'user/:id', component: UserProfileComponent },
    { path: 'quotes', component: MainPageComponent },
    { path: 'users', component: UsersPageComponent },
    { path: '**', component: NotFoundComponent },

];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
