export interface Book {
  id: string;
  title: string;
  authorId: string;
  isbn: string;
  publicationYear: number;
  quantity: number;
  imagePath?: string;
  createdAt: Date;
  updatedAt: Date;
} 