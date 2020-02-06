import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-loggedin',
  templateUrl: './loggedin.component.html',
  styleUrls: ['./loggedin.component.css']
})
export class LoggedinComponent implements OnInit {

  private router: Router;
  private authService: AuthService;
  constructor(router: Router, authService: AuthService) {
    this.router = router;
    this.authService = authService;
   }

  ngOnInit() {
    this.authService.userRegistration();
  }

  startClicked(): boolean {
    this.router.navigate(['/todolist'], { replaceUrl: true });
    return false;
  }
}
