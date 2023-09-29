import { FC, Fragment, ReactNode } from "react";

import { Settings } from "react-slick";

import ProductSlideSkeletonCard from "../card/ProductSlideSkeletonCard";
import SkeletonCardList from "../card/SkeletonCardList";
import ProductCarousel from "../lib/Carousel";

type Props<T> = {
  data: T[] | undefined;
  isLoading: boolean;
  itemRender: (item: T) => ReactNode;

  pageSize?: number;

  carouselSettings?: Settings;

  skeletonCard?: FC;
};

const ProductsCarousel = <T extends any>(props: Props<T>) => {
  const pageSize = props.pageSize || 20;
  // const SkeletonCard = props.skeletonCard || ProductSlideSkeletonCard;

  return (
    <>
      {/* <PageSection
        {...props.pageSectionProps}
        sx={[
          ...(Array.isArray(props.pageSectionProps.sx)
            ? props.pageSectionProps.sx
            : [props.pageSectionProps.sx]),
          {
            display:
              !props.isLoading && !props.data?.length ? "none" : undefined,
          },
        ]}
      >
        
      </PageSection> */}
      {props.isLoading ? (
        <ProductCarousel settings={props.carouselSettings}>
          <SkeletonCardList pageSize={pageSize} skeletonCard={ProductSlideSkeletonCard} />
        </ProductCarousel>
      ) : (
        <ProductCarousel settings={props.carouselSettings}>
          {!!props.data?.length &&
            props.data.map((item, idx) => {
              return (
                <Fragment key={idx}>{props.itemRender(item)}</Fragment>
                // <ProductSlideCard
                //   key={idx}
                //   imgSrc={item.coverImageUrl}
                //   title={item.title}
                //   href={{
                //     pathname: `/works/${item.id}`,
                //     query: {
                //       bookId: item.firstEditionId,
                //     },
                //   }}
                // />
              );
            })}
        </ProductCarousel>
      )}
    </>
  );
};

export default ProductsCarousel;
