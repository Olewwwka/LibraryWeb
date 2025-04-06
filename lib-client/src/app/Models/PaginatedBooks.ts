import {BookModel} from './BookModel';

export interface PaginatedBooks {
  books: BookModel[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}
