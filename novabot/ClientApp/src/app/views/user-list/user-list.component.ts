import { Component, OnInit, Input } from '@angular/core';
import { UserModel } from '../../models/user/UserModel';

@Component({
    selector: 'user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

    @Input() users = <UserModel>{};

    constructor() { }
    ngOnInit() {
    }

}
