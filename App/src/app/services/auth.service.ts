import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import { User } from '../models/user.model';
import { LiteEvent } from '../events/lite-event';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly onUnAuthorizedUser = new LiteEvent<string>();
  private readonly onUserRegistered = new LiteEvent<User>();
  private readonly userCookieName: string = 'user-registered';
  private user: User;
  public get getUser() { return this.user; }
  public get whenUserRegistered() { return this.onUserRegistered.expose(); }
  public get whenUserUnAuthorized() { return this.onUnAuthorizedUser.expose(); }
  constructor(private http: HttpClient, private router: Router, private cookieService: CookieService) { }

  public userRegistration(): void {
    const jsonHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
    const userGetInfoReq = new HttpRequest('GET', '/api/auth/get-user-information', { headers: jsonHeaders });
    this.http.request(userGetInfoReq).subscribe((userData: HttpResponse<any>) => {
      if (userData.ok) {
        const userRegRequ = new HttpRequest('POST', '/api/users',
          JSON.stringify(new User('', userData.body.identifier, userData.body.name)), { headers: jsonHeaders } );
        this.http.request(userRegRequ).subscribe((data: HttpResponse<User>) => {
          if (data.ok) {
            this.onAuthorizedAction(data.body);
          } else {
            this.onUnAuthorizedAction();
          }
        });
      } else {
        this.onUnAuthorizedAction();
      }
    });
  }

  private onAuthorizedAction(userAuthorized: User) {
    this.user = userAuthorized;
    this.onUserRegistered.trigger(userAuthorized);

    if (this.cookieService.check(this.userCookieName)) {
      this.cookieService.delete(this.userCookieName);
    }
    this.cookieService.set(this.userCookieName, JSON.stringify(userAuthorized));
  }

  private onUnAuthorizedAction() {
    this.user = undefined;
    this.onUnAuthorizedUser.trigger('unauthorized');
    this.router.navigateByUrl('/login');

    if (this.cookieService.check(this.userCookieName)) {
      this.cookieService.delete(this.userCookieName);
    }
  }
}
