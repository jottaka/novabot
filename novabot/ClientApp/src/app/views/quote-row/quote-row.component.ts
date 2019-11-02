import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'quote-row',
    templateUrl: './quote-row.component.html',
    styleUrls: ['./quote-row.component.scss']
})
export class QuoteRowComponent implements OnInit {

    @Input() author: string = '';
    @Input() quote = '';
    @Input() thumbnail_url = '';
    @Input() snitch = '';

    constructor() { }

    ngOnInit() {
    }

}
