import { Component, OnInit, Input } from '@angular/core';
import { UserModel } from '../../models/user/UserModel';

@Component({
    selector: 'user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

    @Input() user = <UserModel>{};

    constructor() { }
    ngOnInit() {
    }

}
