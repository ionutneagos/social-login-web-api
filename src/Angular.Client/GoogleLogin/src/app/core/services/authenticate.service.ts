import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable, ReplaySubject } from "rxjs";
import { map } from "rxjs/operators";
import { JwtService } from "./jwt.service";
import { Router } from '@angular/router';
import { UserToken } from '../models/response/user-token';
import { GoogleUserRequest } from '../models/request/google-user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService extends BaseService {

  private currentUserSubject = new BehaviorSubject<UserToken>({} as UserToken);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(httpClient: HttpClient, private jwtService: JwtService, private router: Router) {
    super(httpClient);
    this.currentUserSubject = new BehaviorSubject<UserToken>(this.jwtService.getUser());
    this.currentUser = this.currentUserSubject.asObservable();
  }
  googleLogin(googleUser: GoogleUserRequest): Observable<UserToken> {
    return this.httpClient
      .post<UserToken>(`${this.baseUrl}/api/auth/googleauthenticate`, googleUser)
      .pipe(map(profile => {
        this.setAuth(profile);
        return profile;
      }))
  }

  setAuth(user: UserToken) {
    this.jwtService.saveUser(user);
    this.currentUserSubject.next(user);
  }

  logout() {
    this.purgeAuth();
  }

  purgeAuth() {
    this.jwtService.destroyUser();
    this.currentUserSubject.next(null);
  }
}
