import { RefType, BookStatus } from "./base";

export type SearchProductItem = {
  id: string;
  title: string;
  refType: RefType;

  lendingBookId?: string;
  lendingBookStatus?: BookStatus;

  coverBookId: string;
  coverImageUrl?: string;

  hasFullText: boolean;

  wantToReadCount: number;
  alreadyReadCount: number;

  relatedBookImgs: string[];

  firstPublishYear?: number;
  authors: IdName[];

  ratingsAverage: number;
  ratingsCount: number;

  firstEditionId?: string;
  editionCount: number;
};

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

export type WorkRatings = {
  average: number;
  count: number;
  rating1Stars: number;
  rating2Stars: number;
  rating3Stars: number;
  rating4Stars: number;
  rating5Stars: number;
};
