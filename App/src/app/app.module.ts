import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { TodoItemComponent } from './components/todo-item/todo-item.component';
import { TodoCreateNewItemComponent } from './components/todo-create-new-item/todo-create-new-item.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { LoggedinComponent } from './components/loggedin/loggedin.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    TodoListComponent,
    TodoItemComponent,
    TodoCreateNewItemComponent,
    LoginComponent,
    LoggedinComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/todolist', pathMatch: 'full' },
      { path: 'todolist', component: TodoListComponent },
      { path: 'todolist/create-new-item', component: TodoCreateNewItemComponent },
      { path: 'login', component: LoginComponent },
      { path: 'loggedin', component: LoggedinComponent },
    ]),
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
