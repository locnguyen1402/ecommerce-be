"use client";

import { useEffect, useState } from "react";

import Image from "next/image";

import { Box, alpha, useTheme } from "@mui/material";

import BookPlaceholder from "@/assets/book_placeholder.png";

type Props = {
  src: string | undefined;
  alt: string;
};

const ProductCardAvatar = (props: Props) => {
  const theme = useTheme();
  const [mounted, setMounted] = useState(false);

  useEffect(() => {
    setMounted(true);
  }, []);

  return (
    <Box
      sx={{
        width: "100%",
        height: 220,
        overflow: "hidden",
        borderRadius: 1.5,
        // borderBottomLeftRadius: 0,
        // borderBottomRightRadius: 0,
        padding: 1,
        backgroundColor: mounted
          ? theme.palette.mode === "light"
            ? "grey.200"
            : alpha(theme.palette.grey[500], 0.12)
          : undefined,
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

export default ProductCardAvatar;
