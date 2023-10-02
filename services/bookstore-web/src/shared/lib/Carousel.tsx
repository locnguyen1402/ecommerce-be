import { ReactNode } from "react";

import { Box } from "@mui/material";

import Slider, { type Settings } from "react-slick";

type Props = { settings?: Settings; children: ReactNode };

const Carousel = (props: Props) => {
  const settings: Settings = {
    slidesToShow: 6,
    slidesToScroll: 6,
    infinite: false,
    dots: false,
    arrows: false,
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
    appendDots: (dots) => {
      return (
        <Box
          mt={{
            xs: 4,
            md: 6,
          }}
          position="static"
          component="ul"
          display="flex"
          justifyContent="center"
          alignItems="center"
          sx={{
            "& > .slick-active": {
              ".dot": {
                opacity: 1,
              },
            },
          }}
        >
          {dots}
        </Box>
      );
    },
    customPaging: () => {
      return (
        <Box
          width="100%"
          height="100%"
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Box
            className="dot"
            width={8}
            height={8}
            sx={{
              opacity: 0.32,
              borderRadius: "50%",
              backgroundColor: "primary.main",
            }}
          />
        </Box>
      );
    },
    ...props.settings,
  };

  return <Slider {...settings}>{props.children as any}</Slider>;
};

export default Carousel;
