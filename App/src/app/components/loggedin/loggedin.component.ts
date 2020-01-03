import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loggedin',
  templateUrl: './loggedin.component.html',
  styleUrls: ['./loggedin.component.css']
})
export class LoggedinComponent implements OnInit {

  private router: Router;
  constructor(router: Router) {
    this.router = router;
   }

  ngOnInit() {
  }

  startClicked(): boolean {
    this.router.navigate(['/todolist'], { replaceUrl: true });
    return false;
  }
}
