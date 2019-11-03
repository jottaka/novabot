import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserModel } from '../../models/user/UserModel';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {

    options = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    constructor(private http: HttpClient) {
    }

    getUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>('user/getusers', this.options);
    }

    getUser(userId: string): Observable<UserModel> {
        var myOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
            params: {
                'userId': userId
            }
        }
        return this.http.get<UserModel>('user/getuser',myOptions);
    }
}
