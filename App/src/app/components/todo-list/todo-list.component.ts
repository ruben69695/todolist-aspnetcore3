import { Component, OnInit } from '@angular/core';
import { TodoItem } from 'src/app/models/todo-item.model';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  public todoItemsList: TodoItem[] = [
    new TodoItem('Acabar los deberes', new Date(), null),
    new TodoItem('Hacer Vladimir', new Date(), null),
    new TodoItem('Actualizar repositorio del proyecto Todo List en GitHub', new Date(), null),
    new TodoItem('Pagar 0.87€ por el servidor de Minecraft en PayPal a Fran', new Date(), null)
  ];

  constructor() { }

  ngOnInit() {
  }

}
