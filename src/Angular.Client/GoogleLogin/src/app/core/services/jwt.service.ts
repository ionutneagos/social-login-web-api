import { Injectable } from "@angular/core";
import { UserToken } from '../models/response/user-token';

@Injectable()
export class JwtService {
    
    private static TOKEN_KEY = 'user';

    getUser(): UserToken {
        const user = localStorage.getItem(JwtService.TOKEN_KEY);
        if (user)
            return JSON.parse(user);
        else
            return null;
    }

    saveUser(user: UserToken) {
        localStorage.setItem(JwtService.TOKEN_KEY, JSON.stringify(user));
    }

    destroyUser() {
        localStorage.removeItem(JwtService.TOKEN_KEY);
    }

    getToken(): string {
        const user = this.getUser();
        if (user)
            return user.token;
        else
            return null;
    }
}
