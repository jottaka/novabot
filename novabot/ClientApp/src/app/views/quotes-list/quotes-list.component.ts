import { Component, OnInit, Input } from '@angular/core';
import { ListQuoteResponseModel } from '../../models/quote/listquoteresponsemodel';



@Component({
    selector: 'quotes-list',
    templateUrl: './quotes-list.component.html',
    styleUrls: ['./quotes-list.component.scss']
})
export class QuoteListComponent implements OnInit {

    @Input() quotesList: ListQuoteResponseModel = <ListQuoteResponseModel>{}  ;

    constructor() { }

    ngOnInit() {

        console.log("list");

        console.log(this.quotesList);
    }

}
