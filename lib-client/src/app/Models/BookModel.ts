import { AuthorModel } from './AuthorModel';
import { Genre } from './Genre';

export interface BookModel {
  id: string,
  name: string,
  isbn: string,
  authorId: string,
  author: AuthorModel,
  genre: Genre,
  description: string,
  imagePath: string,
  userId?: string,
  borrowTime?: Date,
  returnTime?: Date,
  isBorrowed?: boolean,
  isOverdue?: boolean
}
