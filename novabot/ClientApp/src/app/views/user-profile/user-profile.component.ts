import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

    @Input() User = {};

    constructor() { }
    ngOnInit() {
    }

}
