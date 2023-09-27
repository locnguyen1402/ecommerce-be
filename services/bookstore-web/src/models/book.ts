import { RefType, BookStatus } from "./base";

export type Entity = {
  id: string;
  title: string;
  subtitle?: string;
  description?: string;

  imageUrlS?: string;
  imageUrlM?: string;

  refType: RefType;

  coverImages: string[];
};

export type Book = Entity & {
  numberOfPages: number;

  status: BookStatus;

  publishCountry?: string;
  publishDate?: string;
  contributions: string[];
  series: string[];
  publishers: string[];
  isbn13: string[];
  isbn10: string[];

  workId?: string;
};

export type Work = Entity & {
  subjects?: string[];
  subjectPeople?: string[];
  subjectTimes?: string[];
  subjectPlaces?: string[];
};
