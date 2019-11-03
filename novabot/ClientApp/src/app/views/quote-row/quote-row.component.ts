import { QuoteFullModel } from '../../models/quote/quotefullmodel';
import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'quote-row',
    templateUrl: './quote-row.component.html',
    styleUrls: ['./quote-row.component.scss']
})
export class QuoteRowComponent implements OnInit {

    @Input() quote: QuoteFullModel = <QuoteFullModel>{} ;

    constructor() { }

    ngOnInit() {
        console.log("Row");
        console.log(this.quote);
    }

}
