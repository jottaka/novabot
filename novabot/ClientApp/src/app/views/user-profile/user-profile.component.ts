import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../models/user/UserModel';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user/user.service'
import { QuoteService } from '../../services/quote/quote.service'
import { ListQuoteResponseModel } from '../../models/quote/ListQuoteResponseModel';
import { ListQuoteRequestModel } from '../../models/quote/ListQuoteRequestModel';
import { OrderByEnum } from '../../models/quote/ListQuoteRequestModel';

@Component({
    selector: 'user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
    user = <UserModel>{};
    quotes = <ListQuoteResponseModel>{}
    constructor(
        private route: ActivatedRoute,
        private userService: UserService,
        private quoteService: QuoteService
    ) { }
    ngOnInit() {
        this.route.params.subscribe(params => this.getUserData(params['id']));
    }
    getUserData(id: string) {

        this.userService.getUser(id).subscribe(
            response => {
                this.user = response;

                if (response.numberOfQuotes > 0) {
                    this.getUserQuotes(id);
                }
            },
            error => {
                console.log(error);
            }
        );


    }

    getUserQuotes(userId: string) {
        
        var request = new ListQuoteRequestModel();
        request.OrderBy = OrderByEnum.ByDate;
        request.UserId = userId;
        request.N = 100;
        request.Page = 0;

        this.quoteService.listQuotesByUser(request, userId)
            .subscribe(
                response => {
                    this.quotes = response;
                    console.log(this.quotes);
                },
                error => {
                    console.log(error);
                }
            );
        }
}
