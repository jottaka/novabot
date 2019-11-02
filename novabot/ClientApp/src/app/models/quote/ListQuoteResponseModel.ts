
import {QuoteFullModel } from './QuoteFullModel'

export class ListQuoteResponseModel {
    TotalQuotes: number;
    NumberOfPages: number;
    Quotes: QuoteFullModel[];
    PageNumber: number;
}