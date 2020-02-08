import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly userCookieName: string = 'user-registered';
  private user: User;
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

  public async registerUser(): Promise<User> {
    const urlUserInfo = '/api/auth/get-user-information';
    const urlRegUser = '/api/users';
    const jsonHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
    let user: User;
    try {
      const userInfo = await this.http
        .get<any>(urlUserInfo, {headers:  jsonHeaders})
        .toPromise();
      const userRegistered = await this.http
        .post<User>(urlRegUser, JSON.stringify(new User('', userInfo.identifier, userInfo.name)), {headers: jsonHeaders})
        .toPromise();
      user = userRegistered;
      this.onAuthorizedAction(userRegistered);
    } catch (error) {
      console.log(error);
      this.onUnAuthorizedAction();
    }
    return user;
  }

  public getUser(): User {
    if (this.cookieService.check(this.userCookieName)) {
      this.user = JSON.parse(this.cookieService.get(this.userCookieName)) as User;
    } else {
      this.user = null;
    }

    return this.user;
  }

  private onAuthorizedAction(userAuthorized: User) {
    this.user = userAuthorized;

    if (this.cookieService.check(this.userCookieName)) {
      this.cookieService.delete(this.userCookieName);
    }
    this.cookieService.set(this.userCookieName, JSON.stringify(userAuthorized), null, null, null, true, 'None');
  }

  private onUnAuthorizedAction() {
    this.user = undefined;
    this.router.navigateByUrl('/login');

    if (this.cookieService.check(this.userCookieName)) {
      this.cookieService.delete(this.userCookieName);
    }
  }
}
