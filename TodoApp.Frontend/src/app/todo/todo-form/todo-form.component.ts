import { Component, input, output } from '@angular/core';
import { NewTodoDto } from '../todo.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-form',
  imports: [FormsModule],
  templateUrl: './todo-form.component.html',
  styleUrl: './todo-form.component.scss'
})
export class TodoFormComponent {
  isAdding = input(false);
  addTodo = output<NewTodoDto>();

  newTodo: NewTodoDto = { title: '', description: '' };

  onSubmit() {
    if (!this.newTodo.title.trim()) {
      return;
    } 
    this.addTodo.emit(this.newTodo);
    this.newTodo = { title: '', description: '' };
  }
}
