import { Component, OnInit, Input } from '@angular/core';
import { UserModel } from '../../models/user/UserModel';
@Component({
    selector: 'user-card',
    templateUrl: './user-card.component.html',
    styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {

    @Input() user: UserModel = <UserModel>{};

    constructor() { }
    ngOnInit() {
    }

}
