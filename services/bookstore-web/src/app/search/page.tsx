import { Box, GlobalStyles, Stack } from "@mui/material";

import qs from "querystring";

import { PRODUCTS_API } from "@/api";
import { adaptPaginationQueryParams } from "@/utils/pagination";
import PageTitle from "@/shared/app/PageTitle";
import PageLayout from "@/shared/layout/PageLayout";
import { SearchProductItem } from "@/models/search";

import FilterSection from "./components/FilterSection";
import FilterDrawerButton from "./components/FilterDrawerButton";
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
          direction="row"
          sx={{
            paddingLeft: { md: 6 },
            width: {
              xs: "100%",
              md: "calc(100% - var(--filter-section-width))",
            },
            height: 24,
            backgroundColor: "red",
          }}
        >
          {response.data.map((item) => {
            return <ProductCard key={item.id} product={item} />;
          })}
        </Stack>
      </Stack>
    </PageLayout>
  );
};

export default SearchPage;
