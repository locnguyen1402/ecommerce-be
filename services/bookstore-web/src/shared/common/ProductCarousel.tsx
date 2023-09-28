import { ReactNode } from "react";

import Slider, { type Settings } from "react-slick";

type Props = { settings?: Settings; children: ReactNode };

const ProductCarousel = (props: Props) => {
  const settings: Settings = {
    slidesToShow: 6,
    slidesToScroll: 6,
    infinite: false,
    dots: false,
    responsive: [
      {
        breakpoint: 1200,
        settings: {
          slidesToShow: 5,
          slidesToScroll: 5,
          infinite: false,
          dots: true,
        },
      },
      {
        breakpoint: 900,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 4,
          infinite: false,
          dots: true,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          infinite: false,
          dots: true,
        },
      },
    ],
    ...props.settings,
  };

  return <Slider {...settings}>{props.children}</Slider>;
};

export default ProductCarousel;
