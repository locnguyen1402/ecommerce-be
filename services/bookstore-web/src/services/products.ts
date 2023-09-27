import qs from "querystring";

import { Work, Book, SearchProductItem } from "@/models";
import { HttpUtils } from "@/utils";
import { PRODUCTS_API } from "@/api";

export const ProductService = {
  searchProducts: (query: PaginationQuery) => {
    const url = `${PRODUCTS_API.Search}?${qs.stringify(query)}`;

    return HttpUtils.get<SearchProductItem[]>(url);
  },

  getWorkDetails: (id: string) => {
    const url = PRODUCTS_API.WorkDetail.replace("{id}", id);

    return HttpUtils.get<Work>(url);
  },

  getFirstInWorkBook: async (workId: string) => {
    const url = `${PRODUCTS_API.FirstInWorkBook.replace("{id}", workId)}`;

    return HttpUtils.get<Book>(url);
  },

  getInWorkBooks: (workId: string, query: PaginationQuery) => {
    const url = `${PRODUCTS_API.InWorkBooks.replace(
      "{id}",
      workId
    )}?${qs.stringify(query)}`;

    return HttpUtils.get<Book[]>(url);
  },

  getBookDetails: (id: string) => {
    const url = PRODUCTS_API.BookDetail.replace("{id}", id);

    return HttpUtils.get<Book>(url);
  },
};
