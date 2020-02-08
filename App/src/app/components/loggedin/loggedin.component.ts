import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { tick } from '@angular/core/testing';

@Component({
  selector: 'app-loggedin',
  templateUrl: './loggedin.component.html',
  styleUrls: ['./loggedin.component.css']
})
export class LoggedinComponent implements OnInit {

  private router: Router;
  private authService: AuthService;
  public userName: string;
  constructor(router: Router, authService: AuthService) {
    this.router = router;
    this.authService = authService;
    this.userName = '';
   }

  async ngOnInit() {
    const user = await this.authService.registerUser();
    this.userName = user.name;
  }

  startClicked(): boolean {
    this.router.navigate(['/todolist'], { replaceUrl: true });
    return false;
  }
}
