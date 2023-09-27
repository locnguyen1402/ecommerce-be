"use client";

import Image from "next/image";

import { Box, BoxProps, alpha, useTheme } from "@mui/material";

import BookPlaceholder from "@/assets/book_placeholder.png";

type Props = {
  src: string | undefined;
  alt: string;

  sx?: BoxProps["sx"];
};

const ProductAvatar = (props: Props) => {
  const theme = useTheme();

  return (
    <Box
      sx={[
        {
          width: "100%",
          height: 220,
          overflow: "hidden",
          borderRadius: 1.5,
          padding: 1,
          backgroundColor: "grey.200",
          [theme.getColorSchemeSelector("dark")]: {
            backgroundColor: alpha(theme.palette.grey[500], 0.12),
          },
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      <Box
        sx={{
          position: "relative",
          width: "100%",
          height: "100%",
        }}
      >
        <Image
          priority
          src={props.src || BookPlaceholder}
          alt={props.alt}
          fill
          sizes="100%"
          style={{
            borderRadius: 4,
            objectFit: "contain",
          }}
        />
      </Box>
    </Box>
  );
};

export default ProductAvatar;
