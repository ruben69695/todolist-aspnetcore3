import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public userName: string;
  constructor(private authService: AuthService) {
    this.userName = '';
  }
  ngOnInit() {
    const user = this.authService.getUser();
    if (user) {
      this.userName = user.name;
    }
  }

  whenUserRegistered(user: User): void {
    if (user) {
      this.userName = user.name;
    }
  }

  whenUserUnAuthorized(): void {
    this.userName = '';
  }
}
