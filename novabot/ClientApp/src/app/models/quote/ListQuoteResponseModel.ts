
import {QuoteFullModel } from './QuoteFullModel'

export class ListQuoteResponseModel {
    totalQuotes: number;
    numberOfPages: number;
    quotes: QuoteFullModel[];
    pageNumber: number;
}