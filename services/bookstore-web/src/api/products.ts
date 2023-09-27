import env from "@/env";

const baseApiUrl = `${env.apiUrl}/api/products`;

export const PRODUCTS_API = {
  Search: baseApiUrl,
  BookDetail: `${baseApiUrl}/books/{id}`,
  WorkDetail: `${baseApiUrl}/works/{id}`,
  InWorkBooks: `${baseApiUrl}/works/{id}/books`,
};
