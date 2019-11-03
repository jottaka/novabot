import { UserModel } from '../../models/user/UserModel'
import { UserService } from '../../services/user/user.service'


import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'users-page',
    templateUrl: './users-page.component.html',
    styleUrls: ['./users-page.component.scss']
})
export class UsersPageComponent implements OnInit {

    public users: UserModel[] = [];
    constructor(
        private userService: UserService
    ) { }

    ngOnInit() {
        this.getUsers();
    }

    getUsers() {
        this.userService.getUsers().subscribe(
            response => {
                this.users = response;
            },
            error => {
                console.log(error);
            }
        );
    }
}
