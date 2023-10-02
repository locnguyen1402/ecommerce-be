"use client";

import { PRODUCTS_API } from "@/api";
import { SearchProductItem } from "@/models";

import QueryExecutor from "../common/QueryExecutor";
import ProductsCarousel from "../common/ProductsCarousel";
import ProductSlideCard from "../card/ProductSlideCard";
import PageSection, { PageSectionProps } from "./PageSection";

type Props = {
  query: Partial<PaginationQuery> & {
    keyword?: string;
  };
  url?: string;

  pageSectionProps: Omit<PageSectionProps, "children">;
};

const ProductsCarouselSection = (props: Props) => {
  return (
    <QueryExecutor<SearchProductItem>
      url={props.url || PRODUCTS_API.Search}
      queryParams={{
        page: 1,
        pageSize: 20,
        ...props.query,
      }}
    >
      {({ isLoading, data }) => {
        return (
          <PageSection
            {...props.pageSectionProps}
            sx={[
              ...(Array.isArray(props.pageSectionProps.sx)
                ? props.pageSectionProps.sx
                : [props.pageSectionProps.sx]),
              {
                display: !isLoading && !data?.length ? "none" : undefined,
              },
            ]}
          >
            <ProductsCarousel
              isLoading={isLoading}
              data={data}
              itemRender={(item) => {
                return (
                  <ProductSlideCard
                    imgSrc={item.coverImageUrl}
                    title={item.title}
                    href={{
                      pathname: `/works/${item.id}`,
                      query: {
                        bookId: item.firstEditionId,
                      },
                    }}
                  />
                );
              }}
            />
          </PageSection>
        );
      }}
    </QueryExecutor>
  );
};

export default ProductsCarouselSection;
