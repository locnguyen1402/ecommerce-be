import { BookStatus, RefType } from "./base";

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
};
