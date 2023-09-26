import Link from "next/link";

import { Stack, Typography } from "@mui/material";

import { SearchProductItem } from "@/models/search";

import ProductCardAvatar from "./ProductCardAvatar";
import ProductCardLikeButton from "./ProductCardLikeButton";

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

      <ProductCardLikeButton />

      <Stack p={1}>
        <Typography
          variant="body1"
          color="text.primary"
          fontWeight="bold"
          sx={{
            textOverflow: "ellipsis",
            overflow: "hidden",
            whiteSpace: "nowrap",
          }}
        >
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
