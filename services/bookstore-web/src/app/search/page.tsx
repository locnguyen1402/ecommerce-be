import { Box, GlobalStyles, Stack } from "@mui/material";

import qs from "querystring";

import { PRODUCTS_API } from "@/api";
import { adaptPaginationQueryParams } from "@/utils/pagination";
import PageTitle from "@/shared/app/PageTitle";
import PageLayout from "@/shared/layout/PageLayout";
import Pagination from "@/shared/common/Pagination";
import { SearchProductItem } from "@/models/search";

import FilterDrawerButton from "./components/FilterDrawerButton";
import FilterSection from "./components/FilterSection";
import ProductCard from "./components/ProductCard";

const getData = async (
  query: PaginationQuery
): Promise<SuccessResponse<SearchProductItem[]>> => {
  const url = `${PRODUCTS_API.Search}?${qs.stringify(query)}`;
  const response = await fetch(url);
  const data = await response.json();

  let pagination: Nullable<PaginationInfo> = null;
  if (!!response.headers.get("X-Pagination")) {
    pagination = JSON.parse(response.headers.get("X-Pagination")!);
  }

  return {
    data,
    meta: {
      pagination,
    },
  };
};

const SearchPage = async (props: PageProps<PaginationQuery>) => {
  const paginationQuery = adaptPaginationQueryParams(props.searchParams);
  const response = await getData(paginationQuery);

  return (
    <PageLayout>
      <GlobalStyles
        styles={{
          ":root": {
            "--filter-section-width": "280px",
          },
        }}
      />
      <Stack
        py={5}
        direction="row"
        justifyContent="space-between"
        alignItems="center"
      >
        <PageTitle text="Books" />
        <FilterDrawerButton />
      </Stack>

      <Stack direction="row">
        <Box
          display={{
            xs: "none",
            md: "flex",
          }}
        >
          <FilterSection />
        </Box>

        <Stack
          sx={{
            paddingLeft: { md: 4 },
            width: {
              xs: "100%",
              md: "calc(100% - var(--filter-section-width))",
            },
            paddingBottom: 8,
          }}
        >
          <Box
            sx={{
              width: "100%",
              display: "grid",
              gridTemplateColumns: {
                xs: "repeat(2, 1fr)",
                sm: "repeat(3, 1fr)",
                md: "repeat(3, 1fr)",
                lg: "repeat(4, 1fr)",
              },
              gap: {
                xs: 2,
                lg: 3,
              },
            }}
          >
            {response.data.map((item) => {
              return <ProductCard key={item.id} product={item} />;
            })}
          </Box>

          {!!response.meta?.pagination && (
            <Box display="flex" justifyContent="center" mt={4}>
              <Pagination info={response.meta?.pagination} />
            </Box>
          )}
        </Stack>
      </Stack>
    </PageLayout>
  );
};

export default SearchPage;
