"use client";

import { Box, BoxProps, alpha, useTheme } from "@mui/material";

import AspectRatio from "./AspectRatio";

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
          borderRadius: 4,
          padding: 1,
          backgroundColor: "grey.200",
          [theme.getColorSchemeSelector("dark")]: {
            backgroundColor: alpha(theme.palette.grey[500], 0.12),
          },
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      <AspectRatio ratio={12 / 16}>
        <Box
          component="img"
          src={props.src || "/assets/book_placeholder.png"}
          alt={props.alt}
          sx={{
            width: "100%",
            height: "100%",
            objectFit: "contain",
          }}
        />
      </AspectRatio>
    </Box>
  );
};

export default ProductAvatar;
