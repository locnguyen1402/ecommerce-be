import { RefType, BookStatus } from "./base";

export type Book = {
  id: string;
  title: string;
  subtitle?: string;
  description?: string;
  subjects?: string[];
  subjectPeople?: string[];
  subjectTimes?: string[];

  numberOfPages: number;

  status: BookStatus;
  refType: RefType;

  imageUrlS?: string;
  imageUrlM?: string;

  publishCountry?: string;
  publishDate?: string;
  contributions: string[];
  series: string[];
  publishers: string[];
  isbn13: string[];
  isbn10: string[];
};
