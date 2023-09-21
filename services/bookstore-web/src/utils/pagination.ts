export const adaptPaginationQueryParams = <Q extends PaginationQuery>(
  query: Q
): PaginationQuery => {
  let currentPage = 1;
  let pageSize = 10;

  if (Number(query.page) > 1) {
    currentPage = Number(query.page);
  }

  if (Number.isInteger(query.pageSize)) {
    pageSize = Number(query.pageSize);
  }

  return {
    page: currentPage,
    pageSize,
    keyword: query?.keyword,
  };
};
