import { Suspense } from "react";

import { Box, GlobalStyles, Stack } from "@mui/material";

import { adaptPaginationQueryParams } from "@/utils";
import { ProductService } from "@/services";

import PageTitle from "@/shared/app/PageTitle";
import PageLayout from "@/shared/layout/PageLayout";
import PromiseResolver from "@/shared/common/PromiseResolver";

import FilterDrawerButton from "./components/FilterDrawerButton";
import FilterSection from "./components/FilterSection";
import ProductList from "./components/ProductList";
import SkeletonProductList from "./components/SkeletonProductList";

const SearchPage = (props: PageProps<PaginationQuery>) => {
  const paginationQuery = adaptPaginationQueryParams(props.searchParams);

  const searchResultPromise = ProductService.searchProducts(paginationQuery);

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
        py={{
          xs: 2,
          md: 5,
        }}
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
          <div key={Math.random().toString()}>
            <Box
              sx={{
                width: "100%",
                display: "grid",
                gridTemplateColumns: {
                  xs: "repeat(2, minmax(0, 1fr))",
                  sm: "repeat(3, minmax(0, 1fr))",
                  md: "repeat(3, minmax(0, 1fr))",
                  lg: "repeat(4, minmax(0, 1fr))",
                },
                gap: {
                  xs: 2,
                  lg: 3,
                },
              }}
            >
              <Suspense
                fallback={
                  <SkeletonProductList pageSize={paginationQuery.pageSize} />
                }
              >
                <PromiseResolver promise={searchResultPromise}>
                  {(val) => <ProductList {...val} />}
                </PromiseResolver>
              </Suspense>
            </Box>
          </div>
        </Stack>
      </Stack>
    </PageLayout>
  );
};

export default SearchPage;
