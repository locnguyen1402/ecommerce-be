import { Suspense } from "react";

import { redirect } from "next/navigation";
import { RedirectType } from "next/dist/client/components/redirect";

import { Box } from "@mui/material";

import { adaptPaginationQueryParams } from "@/utils";
import { ProductService } from "@/services";
import { TrendingType } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import PageHeader from "@/shared/app/PageHeader";
import PromiseResolver from "@/shared/common/PromiseResolver";

import SkeletonProductList from "@/app/search/components/SkeletonProductList";
import ProductList from "@/app/search/components/ProductList";

const TrendingPage = (
  props: PageProps<
    PaginationQuery,
    {
      type: TrendingType;
    }
  >
) => {
  const paginationQuery = adaptPaginationQueryParams(props.searchParams);
  if (!Object.values(TrendingType).includes(props.params.type)) {
    return redirect("/not-found", RedirectType.replace);
  }

  const resultPromise = ProductService.getTrendingProducts({
    ...paginationQuery,
    type: props.params.type,
  });

  return (
    <PageLayout>
      <PageHeader
        title={`Trending Books: ${props.params.type.toUpperCase()}`}
      />

      <div key={Math.random().toString()}>
        <Box
          sx={{
            width: "100%",
            display: "grid",
            gridTemplateColumns: {
              xs: "repeat(2, minmax(0, 1fr))",
              sm: "repeat(3, minmax(0, 1fr))",
              md: "repeat(4, minmax(0, 1fr))",
              lg: "repeat(5, minmax(0, 1fr))",
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
            <PromiseResolver promise={resultPromise}>
              {(val) => <ProductList {...val} />}
            </PromiseResolver>
          </Suspense>
        </Box>
      </div>
    </PageLayout>
  );
};

export default TrendingPage;
