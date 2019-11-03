import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ListQuoteRequestModel } from '../../models/quote/ListQuoteRequestModel';
import { ListQuoteResponseModel } from '../../models/quote/ListQuoteResponseModel';


import { Observable } from 'rxjs';



@Injectable()
export class QuoteService {

    options = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    constructor(private http: HttpClient) {
    }

    listQuotes(request: ListQuoteRequestModel): Observable<ListQuoteResponseModel> {

        return this.http.post<ListQuoteResponseModel>('quotes/list',request,this.options);
    }

    listQuotesByUser(request: ListQuoteRequestModel, userId: string): Observable<ListQuoteResponseModel> {
        var myOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
            params: {
                'userId': userId
            }
        }
        return this.http.post<ListQuoteResponseModel>('quotes/ListByUser', request, myOptions);
    }

    listQuotesBySnitch(request: ListQuoteRequestModel, snitchId: string): Observable<ListQuoteResponseModel> {
        var myOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
            params: {
                'snitchId': snitchId
            }
        }
        return this.http.post<ListQuoteResponseModel>('quotes/ListBySnitch', request, myOptions);
    }
}
