import env from "@/env";

const baseApiUrl = `${env.apiUrl}/api/products`;

export const PRODUCTS_API = {
  Search: baseApiUrl,
  ProductBookDetail: `${baseApiUrl}/books/{id}`,
  ProductWorkDetail: `${baseApiUrl}/works/{id}`,
};
