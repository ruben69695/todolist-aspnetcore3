import { Injectable } from '@angular/core';
import { TodoItem } from 'src/app/models/todo-item.model';

@Injectable({
    providedIn: 'root',
})
export class TodoItemsService {
    public todoItemsList: TodoItem[];

    constructor() {
        this.todoItemsList = [
            new TodoItem('Acabar los deberes', new Date(), null),
            new TodoItem('Hacer Vladimir', new Date(), null),
            new TodoItem('Actualizar repositorio del proyecto Todo List en GitHub', new Date(), null),
            new TodoItem('Pagar 0.87€ por el servidor de Minecraft en PayPal a Fran', new Date(), null)
          ];
    }

    add(item: TodoItem): boolean {
        this.todoItemsList.push(item);
        return true;
    }

    remove(item: TodoItem): boolean {
        this.todoItemsList = this.todoItemsList.filter(x => x.finish_date == null);
        return true;
    }
}
