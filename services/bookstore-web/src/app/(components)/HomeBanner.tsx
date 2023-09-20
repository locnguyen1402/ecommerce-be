"use client";

import { Box, Grid, Typography } from "@mui/material";

const HomeBanner = () => {
  return (
    <Box
      paddingY={15}
      sx={{
        backgroundColor: (theme) =>
          `rgba(${theme.vars.palette.background.default}, 0.9)`,
      }}
    >
      <Grid container spacing={3}>
        <Grid item xs={12} md={6} lg={5}>
          <Typography
            component="h1"
            variant="h2"
            sx={{
              fontWeight: "bold",
            }}
          >
            Free Knowledge
            <br />
            From{" "}
            <Box
              component="span"
              sx={{
                textDecoration: "underline",
                color: "primary.main",
              }}
            >
              Vi Books
            </Box>
          </Typography>
        </Grid>
        <Grid item xs={false} md={6} lg={7}></Grid>
      </Grid>
    </Box>
  );
};

export default HomeBanner;
