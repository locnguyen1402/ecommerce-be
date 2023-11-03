const baseUrl = "/api/ol-products";

export const PRODUCTS_API = {
  Search: `${baseUrl}/works`,
  Trending: `${baseUrl}/trending`,
  BookDetail: `${baseUrl}/books/{id}`,
  WorkDetail: `${baseUrl}/works/{id}`,
  WorkRatings: `${baseUrl}/works/{id}/ratings`,
  InWorkBooks: `${baseUrl}/works/{id}/books`,
  FirstInWorkBook: `${baseUrl}/works/{id}/books/first`,
};
