import Link from "next/link";

import { IconButton, Stack, Typography } from "@mui/material";
import { Favorite as FavoriteIcon } from "@mui/icons-material";

import { SearchProductItem } from "@/models/search";

import ProductCardAvatar from "./ProductCardAvatar";

type Props = {
  product: SearchProductItem;
};

const ProductCard = (props: Props) => {
  const { product } = props;

  return (
    <Stack
      sx={{
        position: "relative",
        textDecoration: "none",
        transition: "all .2s ease-in-out",
        ":hover": {
          borderRadius: 1.5,
          boxShadow: 1,
          transform: "scale(1.1)",
          zIndex: 1,

          ".favorite-btn": {
            display: "inline-flex",
          },
        },
      }}
      component={Link}
      href={`/products/${product.id}`}
    >
      <ProductCardAvatar src={product.coverImageUrl} alt={product.title} />

      <IconButton
        className="favorite-btn"
        aria-label="add-to-favorites"
        sx={{
          display: {
            xs: "inline-flex",
            md: "none",
          },
          minWidth: "unset",
          position: "absolute",
          right: 5,
          top: 180,
          backgroundColor: "primary.main",
          color: "#fff",

          ":hover": {
            backgroundColor: "primary.main",
          },
        }}
        size="small"
      >
        <FavoriteIcon />
      </IconButton>

      <Stack p={1}>
        <Typography variant="body1" color="text.primary" fontWeight="bold">
          {product.title}
        </Typography>

        {!!product.firstPublishYear && (
          <Typography variant="caption" color="text.secondary">
            First published in {product.firstPublishYear}
          </Typography>
        )}
      </Stack>
    </Stack>
  );
};

export default ProductCard;
