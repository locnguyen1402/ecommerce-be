import { Suspense } from "react";

import { Box, GlobalStyles, Stack } from "@mui/material";

import { adaptPaginationQueryParams } from "@/utils";
import { ProductService } from "@/services";

import PageHeader from "@/shared/app/PageHeader";
import PageLayout from "@/shared/layout/PageLayout";
import PromiseResolver from "@/shared/common/PromiseResolver";
import Pagination from "@/shared/common/Pagination";
import SkeletonCardList from "@/shared/card/SkeletonCardList";
import SearchProductItemCard from "@/shared/card/SearchProductItemCard";
import SearchProductItemSkeletonCard from "@/shared/card/SearchProductItemSkeletonCard";

import FilterDrawerButton from "./components/FilterDrawerButton";
import FilterSection from "./components/FilterSection";

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
      <PageHeader title="Books" action={<FilterDrawerButton />} />

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
                  <SkeletonCardList
                    pageSize={paginationQuery.pageSize}
                    skeletonCard={SearchProductItemSkeletonCard}
                  />
                }
              >
                <PromiseResolver promise={searchResultPromise}>
                  {({ data, meta }) => (
                    <>
                      {!!data.length && (
                        <>
                          {data.map((item, idx) => {
                            return (
                              <SearchProductItemCard
                                key={item.id}
                                product={item}
                              />
                            );
                          })}

                          {!!meta?.pagination && (
                            <Box
                              gridColumn={{
                                xs: "span 2",
                                sm: "span 3",
                                lg: "span 4",
                              }}
                              display="flex"
                              justifyContent="center"
                              mt={{
                                xs: 2,
                                md: 4,
                              }}
                            >
                              <Pagination info={meta?.pagination} />
                            </Box>
                          )}
                        </>
                      )}
                    </>
                  )}
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
