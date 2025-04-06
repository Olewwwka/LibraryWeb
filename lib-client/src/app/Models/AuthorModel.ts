export interface AuthorModel {
  id: string;
  name: string;
  surname: string;
  birthday: Date;
  country: string;
  books?: any[];
} 