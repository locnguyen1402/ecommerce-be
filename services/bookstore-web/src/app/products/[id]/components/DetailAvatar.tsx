"use client";

import Image from "next/image";

import { Box, alpha, useTheme } from "@mui/material";

import { Book } from "@/models";

import BookPlaceholder from "@/assets/book_placeholder.png";

type Props = {
  product: Book;
};

const DetailAvatar = (props: Props) => {
  const theme = useTheme();
  const { product } = props;
  console.log(
    "🚀 ~ file: DetailAvatar.tsx:18 ~ DetailAvatar ~ product:",
    product
  );

  return (
    <Box
      sx={{
        width: "100%",
        height: {
          xs: 300,
          sm: 360,
          md: 420,
        },
        overflow: "hidden",
        borderRadius: 1.5,
        padding: 2,
        backgroundColor: "grey.200",
        [theme.getColorSchemeSelector("dark")]: {
          backgroundColor: alpha(theme.palette.grey[500], 0.12),
        },
      }}
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
          src={product.imageUrlM || BookPlaceholder}
          alt={product.title}
          fill
          sizes="100%"
          style={{
            objectFit: "contain",
          }}
        />
      </Box>
    </Box>
  );
};

export default DetailAvatar;
