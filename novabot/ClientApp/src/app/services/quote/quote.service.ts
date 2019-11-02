import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';



import { QuoteFullModel } from '../../models/quote/QuoteFullModel';
import { RequestOptions } from "@angular/http/http";
import { ListQuoteRequestModel,OrderByEnum } from '../../models/quote/ListQuoteRequestModel';
import { ListQuoteResponseModel } from '../../models/quote/ListQuoteResponseModel';


import { Observable } from 'rxjs';
import { of } from 'rxjs';



@Injectable()
export class QuoteService {

    options = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    constructor(private http: HttpClient) {
    }

    listQuotes(request: ListQuoteRequestModel): Observable<QuoteFullModel[]> {

        return this.http.post<QuoteFullModel[]>('qutes/list',request,this.options);
    }
}
