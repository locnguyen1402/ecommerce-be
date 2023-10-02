import { FC, Fragment, ReactNode, useMemo } from "react";

import { Settings } from "react-slick";

import ProductSlideSkeletonCard from "../card/ProductSlideSkeletonCard";
import Carousel from "../lib/Carousel";

type Props<T extends Record<string, any>> = {
  data: T[] | undefined;
  isLoading: boolean;
  itemRender: (item: T) => ReactNode;
  keyField?: keyof T;

  pageSize?: number;

  carouselSettings?: Settings;

  skeletonCard?: FC;
};

const ProductsCarousel = <T extends Record<string, any>>(props: Props<T>) => {
  const pageSize = props.pageSize || 20;
  const SkeletonCard = useMemo(
    () => props.skeletonCard || ProductSlideSkeletonCard,
    [props.skeletonCard]
  );

  return (
    <>
      {props.isLoading ? (
        <Carousel settings={props.carouselSettings}>
          {Array.from(Array(pageSize).keys()).map((_, idx) => {
            return <SkeletonCard key={idx} />;
          })}
        </Carousel>
      ) : (
        <Carousel settings={props.carouselSettings}>
          {!!props.data?.length &&
            props.data.map((item, idx) => {
              return (
                <Fragment key={item[props.keyField || "id"] as string}>
                  {props.itemRender(item)}
                </Fragment>
              );
            })}
        </Carousel>
      )}
    </>
  );
};

export default ProductsCarousel;
