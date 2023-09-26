import qs from "querystring";

import { HttpUtils } from "@/utils";
import { Book, SearchProductItem } from "@/models";
import { PRODUCTS_API } from "@/api";

export const ProductService = {
  searchProducts: (query: PaginationQuery) => {
    const url = `${PRODUCTS_API.Search}?${qs.stringify(query)}`;

    return HttpUtils.get<SearchProductItem[]>(url);
  },

  getDetails: (id: string) => {
    const url = PRODUCTS_API.ProductWorkDetail.replace("{id}", id);
    console.log("🚀 ~ file: products.ts:16 ~ url:", url);

    return HttpUtils.get<Book>(url);
  },
};
