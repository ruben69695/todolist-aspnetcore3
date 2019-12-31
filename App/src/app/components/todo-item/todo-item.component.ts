import { Component, OnInit, Input } from '@angular/core';
import { TodoItem } from './../../models/todo-item.model';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css']
})
export class TodoItemComponent implements OnInit {

  @Input() public todoItem: TodoItem;

  constructor() { }

  ngOnInit() {
  }

  public completeTask() {
    console.log('Completed');
    if (!this.todoItem.isCompleted()) {
      this.todoItem.finish_date = new Date();
    }
  }

}
