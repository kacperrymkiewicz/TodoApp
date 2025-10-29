import { Component, inject, OnInit, signal } from '@angular/core';
import { TodoService } from './todo/todo.service';
import { NewTodoDto, TodoItem } from './todo/todo.model';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { TodoFormComponent } from "./todo/todo-form/todo-form.component";
import { TodoListComponent } from "./todo/todo-list/todo-list.component";

@Component({
  selector: 'app-root',
  imports: [
    FormsModule,
    TodoFormComponent,
    TodoListComponent
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private todoService = inject(TodoService);

  todos = signal<TodoItem[]>([]);
  isLoading = signal(false);
  isAdding = signal(false);
  error = signal(false);

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.isLoading.set(true);
    this.error.set(false);

    this.todoService
      .getTodos()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (response) => this.todos.set(response),
        error: () => this.error.set(true)
      });
  }

  addTodo = (newTodo : NewTodoDto) => {
    this.isAdding.set(true);
    this.todoService
      .addTodo(newTodo)
      .pipe(finalize(() => this.isAdding.set(false)))
      .subscribe({
        next: (todo) => this.todos.update((list) => [todo, ...list])
      });
  }

  toggleTodoStatus = (item: TodoItem) => {
    const updated = {
      ...item,
      isCompleted: !item.isCompleted
    };

    this.todoService.updateTodo(updated.id, updated).subscribe({
      next: () => {
        this.todos.update((list) => 
          list.map((t) => (t.id == updated.id ? updated : t))
        )
      }
    })
  }
}

