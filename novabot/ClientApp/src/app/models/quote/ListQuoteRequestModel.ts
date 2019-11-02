
export class ListQuoteRequestModel {
    OrderBy: OrderByEnum;
    Page: number;
    N: number;
    UserId: string;
}

export enum OrderByEnum {
    ByName = 1,
    ByPositiveVotes = 2,
    ByNegativeVotes = 3,
    ByDate = 4
}