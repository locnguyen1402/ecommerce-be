"use client";

import { useQuery } from "@tanstack/react-query";

import { ProductService } from "@/services";
import { ProductSearchQuery } from "@/models";

import ProductCarousel from "@/shared/common/ProductCarousel";
import ProductSlideCard from "@/shared/common/ProductSlideCard";
import ProductSlideCardSkeleton from "@/shared/common/ProductSlideCardSkeleton";
import PageSection, { type PageSectionProps } from "@/shared/app/PageSection";

type Props = {
  query: Partial<ProductSearchQuery>;
  pageSectionProps: Omit<PageSectionProps, "children">;
};

const RelatedProducts = (props: Props) => {
  const pageSize = props.query.pageSize || 20;
  const context = useQuery({
    queryKey: ["RelatedProducts"],
    queryFn: () => {
      return ProductService.searchProducts({
        page: 1,
        pageSize,
        ...props.query,
      });
    },
  });

  return (
    <>
      <PageSection
        {...props.pageSectionProps}
        sx={[
          ...(Array.isArray(props.pageSectionProps.sx)
            ? props.pageSectionProps.sx
            : [props.pageSectionProps.sx]),
          {
            display:
              !context.isLoading && !context.data?.data.length
                ? "none"
                : undefined,
          },
        ]}
      >
        {context.isLoading ? (
          <ProductCarousel>
            {Array.from(Array(pageSize).keys()).map((_, idx) => {
              return <ProductSlideCardSkeleton key={idx} />;
            })}
          </ProductCarousel>
        ) : (
          <ProductCarousel>
            {context.data?.data.map((item, idx) => {
              return (
                <ProductSlideCard
                  key={idx}
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
            })}
          </ProductCarousel>
        )}
      </PageSection>
    </>
  );
};

export default RelatedProducts;
