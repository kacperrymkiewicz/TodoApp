export interface TodoItem {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
}

export type NewTodoDto = Omit<TodoItem, 'id' | 'isCompleted'>;
