import { Component, OnInit, HostBinding } from '@angular/core';
import { TodoItemsService } from 'src/app/services/todoitems.service';
import { TodoItem } from 'src/app/models/todo-item.model';
import { trigger, state, style, transition, animate, AnimationEvent } from '@angular/animations';

@Component({
  selector: 'app-todo-create-new-item',
  templateUrl: './todo-create-new-item.component.html',
  styleUrls: ['./todo-create-new-item.component.css'],
  animations: [
    trigger('showHide', [
      state('show', style({
        opacity: 1
      })),
      state('hide', style({
        opacity: 0
      })),
      transition('show => hide', [
        animate('2s')
      ]),
      transition('hide => show', [
        animate('1s')
      ])
    ])
  ]
})
export class TodoCreateNewItemComponent implements OnInit {

  private todoService: TodoItemsService;
  public sucessIsHidden: boolean;
  public description: string;


  constructor(todoService: TodoItemsService) {
    this.todoService = todoService;
    this.description = '';
    this.sucessIsHidden = true;
  }

  ngOnInit() {
  }

  onCreateTodoItem(): boolean {
    if (this.description !== '') {
      this.todoService.add(new TodoItem(this.description, new Date(), null));
      this.sucessIsHidden = false;
    }
    return false;
  }

  onAnimationDoneEvent(event: AnimationEvent) {
    if (event.fromState === 'hide' && event.toState === 'show') {
      this.sucessIsHidden = true;
      console.log('animation end');
    }
  }

}
