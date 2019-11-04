"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var not_found_component_1 = require("./views/errors/not-found/not-found.component");
var user_profile_component_1 = require("./views/user-profile/user-profile.component");
var main_page_component_1 = require("./main-layout/main-page/main-page.component");
var users_page_component_1 = require("./main-layout/users-page/users-page.component");
var routes = [
    { path: '', pathMatch: 'full', redirectTo: 'quotes' },
    { path: 'user/:id', component: user_profile_component_1.UserProfileComponent },
    { path: 'quotes', component: main_page_component_1.MainPageComponent },
    { path: 'users', component: users_page_component_1.UsersPageComponent },
    { path: '**', component: not_found_component_1.NotFoundComponent },
];
exports.AppRoutes = router_1.RouterModule.forRoot(routes);
//# sourceMappingURL=app.routes.service.js.map