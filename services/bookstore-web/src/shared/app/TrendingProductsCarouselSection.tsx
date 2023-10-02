import { PRODUCTS_API } from "@/api";
import { TrendingType } from "@/models";

import { PageSectionProps } from "./PageSection";
import ProductsCarouselSection from "./ProductsCarouselSection";

type Props = {
  query: Partial<PaginationQuery> & {
    type?: TrendingType;
  };

  pageSectionProps: Omit<PageSectionProps, "children">;
};

const TrendingProductsCarouselSection = (props: Props) => {
  return (
    <ProductsCarouselSection
      url={PRODUCTS_API.Trending}
      query={props.query}
      pageSectionProps={props.pageSectionProps}
    />
  );
};

export default TrendingProductsCarouselSection;
