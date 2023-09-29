export type ProductSearchQuery = PaginationQuery & {
  title?: string;
  author?: string;
  subject?: string;
  place?: string;
  person?: string;
  hasFullText?: boolean;
};
