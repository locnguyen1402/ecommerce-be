"use client";

import { Button } from "@mui/material";
import { Favorite as FavoriteIcon } from "@mui/icons-material";

const ProductCardLikeButton = () => {
  return (
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
  );
};

export default ProductCardLikeButton;
