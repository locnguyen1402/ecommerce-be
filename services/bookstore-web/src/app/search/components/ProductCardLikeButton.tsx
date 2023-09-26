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
        top: 180,
        borderRadius: "50%",
        width: 40,
        height: 40,
        color: "white",
      }}
      onClick={(evt) => {
        evt.preventDefault();
      }}
    >
      <FavoriteIcon />
    </Button>
  );
};

export default ProductCardLikeButton;
