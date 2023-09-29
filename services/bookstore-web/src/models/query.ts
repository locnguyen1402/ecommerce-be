import { TrendingType } from "./base";

export type ProductSearchQuery = PaginationQuery & {
  title?: string;
  author?: string;
  subject?: string;
  place?: string;
  person?: string;
  hasFullText?: boolean;
};

export type TrendingProductsQuery = PaginationQuery & {
  type?: TrendingType;
};
