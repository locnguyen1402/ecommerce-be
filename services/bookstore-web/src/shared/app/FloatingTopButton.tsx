"use client";

import { Fab, useScrollTrigger } from "@mui/material";
import { Navigation as NavigationIcon } from "@mui/icons-material";

const FloatingTopButton = () => {
  const isScrolled = useScrollTrigger({
    disableHysteresis: true,
    threshold: 100,
  });

  return (
    <Fab
      color="primary"
      aria-label="top"
      size="medium"
      sx={{
        display: !isScrolled ? "none" : undefined,
        position: "fixed",
        bottom: {
          xs: 55,
          md: 30,
        },
        right: {
          xs: 10,
          md: 30,
        },
      }}
      onClick={() => {
        window.scrollTo({
          top: 0,
          behavior: "smooth",
        });
      }}
    >
      <NavigationIcon sx={{ color: "white" }} />
    </Fab>
  );
};

export default FloatingTopButton;
