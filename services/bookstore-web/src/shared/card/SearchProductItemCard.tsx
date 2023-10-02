"use client";

import Link from "next/link";

import { Box, Rating, Stack, Typography, Button } from "@mui/material";
import { Favorite as FavoriteIcon } from "@mui/icons-material";

import { SearchProductItem } from "@/models";
import ProductAvatar from "@/shared/common/ProductAvatar";

type Props = {
  product: SearchProductItem;
};

const SearchProductItemCard = (props: Props) => {
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
        <Button
          color="primary"
          variant="contained"
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
            bottom: 5,
            borderRadius: "50%",
            width: {
              xs: 32,
              md: 40,
            },
            height: {
              xs: 32,
              md: 40,
            },
            color: "white",
          }}
          onClick={(evt) => {
            evt.preventDefault();
            evt.stopPropagation();
          }}
        >
          <FavoriteIcon />
        </Button>
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

export default SearchProductItemCard;
