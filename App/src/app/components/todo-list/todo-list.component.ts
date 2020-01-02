import { Component, OnInit } from '@angular/core';
import { TodoItem } from 'src/app/models/todo-item.model';
import { TodoItemsService } from 'src/app/services/todoitems.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  private todoItemsService: TodoItemsService;

  constructor(todoItemsService: TodoItemsService) {
    this.todoItemsService = todoItemsService;
  }

  ngOnInit() {
  }

}
