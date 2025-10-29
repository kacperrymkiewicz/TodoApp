import { Component, input, output } from '@angular/core';
import { TodoItem } from '../todo.model';

@Component({
  selector: 'app-todo-item',
  imports: [],
  host: {
    class: 'list-group-item d-flex justify-content-between align-items-center',
    '[class.list-group-item-primary]': 'item().isCompleted'
  },
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss'
})
export class TodoItemComponent {
  item = input.required<TodoItem>();
  statusChange = output<TodoItem>();

  toggleStatus() {
    this.statusChange.emit(this.item());
  }
}
