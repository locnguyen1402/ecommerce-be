import env from "@/env";

const baseApiUrl = `${env.apiUrl}/api/products`;

export const PRODUCTS_API = {
  Search: baseApiUrl,
  BookDetail: `${baseApiUrl}/books/{id}`,
  WorkDetail: `${baseApiUrl}/works/{id}`,
  WorkRatings: `${baseApiUrl}/works/{id}/ratings`,
  InWorkBooks: `${baseApiUrl}/works/{id}/books`,
  FirstInWorkBook: `${baseApiUrl}/works/{id}/books/first`,
};
