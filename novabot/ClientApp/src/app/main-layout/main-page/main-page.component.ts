import { QuoteService } from '../../services/quote/quote.service'
//import {QuoteFullModel } from '../../models/quote/quotefullmodel'
import {ListQuoteResponseModel } from '../../models/quote/listquoteresponsemodel'
import {ListQuoteRequestModel,OrderByEnum } from '../../models/quote/listquoterequestmodel'
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
    selector: 'main-page',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.scss'],
    changeDetection:ChangeDetectionStrategy.OnPush
})
export class MainPageComponent implements OnInit {

    myQuotes: ListQuoteResponseModel = <ListQuoteResponseModel>{};
    constructor(
        private quoteService: QuoteService,
        public cd: ChangeDetectorRef
    ) { }
    ngOnInit() {
        this.getQuotesList();
    }

    getQuotesList() {
        let request = new ListQuoteRequestModel();
        request.N = 10;
        request.OrderBy = OrderByEnum.ByDate;
        request.Page = 0;

        this.quoteService.listQuotes(request)
            .subscribe(
                response => {
                    this.myQuotes = response;
                    this.cd.detectChanges();
                    console.log(this.myQuotes);
                },
                error => {
                    console.log(error);
                }
            );

    }

    



}
