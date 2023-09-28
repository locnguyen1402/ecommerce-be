"use client";

import { useQuery } from "@tanstack/react-query";

import { ProductService } from "@/services";

import ProductCarousel from "@/shared/common/ProductCarousel";
import ProductSlideCard from "@/shared/common/ProductSlideCard";

const RelatedProducts = () => {
  const context = useQuery({
    queryKey: ["RelatedProducts"],
    queryFn: () => {
      return ProductService.searchProducts({
        keyword: "love from",
        pageSize: 20,
        page: 1,
      });
    },
  });

  return (
    <ProductCarousel>
      {context.data?.data.map((item) => {
        return (
          <ProductSlideCard
            key={item.id}
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
  );
};

export default RelatedProducts;
