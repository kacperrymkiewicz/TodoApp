import { Component, input, output } from '@angular/core';
import { TodoItem } from '../todo.model';
import { TodoItemComponent } from "../todo-item/todo-item.component";

@Component({
  selector: 'app-todo-list',
  imports: [TodoItemComponent],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.scss'
})
export class TodoListComponent {
  todos = input<TodoItem[]>([]);
  isLoading = input(false);
  error = input(false);

  reload = output<void>();
  statusChange = output<TodoItem>();
}
