import Link from "next/link";

import { Box, Rating, Stack, Typography } from "@mui/material";

import { SearchProductItem } from "@/models";
import ProductAvatar from "@/shared/common/ProductAvatar";

import ProductCardLikeButton from "./ProductCardLikeButton";

type Props = {
  product: SearchProductItem;
};

const ProductCard = (props: Props) => {
  const { product } = props;

  return (
    <Stack
      sx={{
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
      href={{
        pathname: `/works/${product.id}`,
        query: {
          bookId: product.firstEditionId,
        },
      }}
    >
      <Box
        sx={{
          width: "100%",
          position: "relative",
        }}
      >
        <ProductAvatar src={product.coverImageUrl} alt={product.title} />
        <ProductCardLikeButton />
      </Box>

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
        {!!product.ratingsCount && (
          <Stack direction="row" alignItems="center" spacing={0.5}>
            <Rating value={product.ratingsAverage} readOnly size="small" />
            <Typography variant="caption" color="text.secondary">
              {product.ratingsCount} reviews
            </Typography>
          </Stack>
        )}
      </Stack>
    </Stack>
  );
};

export default ProductCard;
