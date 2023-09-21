"use client";

import Image from "next/image";
import Link from "next/link";

import { Box, Button, Container, Grid, Stack, Typography } from "@mui/material";

import bannerImg from "@/assets/home/banner.png";

const HomeBanner = () => {
  return (
    <Box
      paddingY={15}
      sx={{
        background: (theme) =>
          `linear-gradient(rgba(${theme.vars.palette.background.defaultChannel}, 0.9), rgba(${theme.vars.palette.background.defaultChannel}, 0.9)), url(/assets/overlay_1.jpg) no-repeat`,
      }}
    >
      <Container>
        <Grid
          container
          spacing={{
            xs: 0,
            md: 3,
          }}
        >
          <Grid item xs={12} md={6} lg={5}>
            <Typography
              textAlign={{
                xs: "center",
                md: "left",
              }}
              component="h1"
              variant="h1"
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
            <Typography
              color={(theme) => theme.vars.palette.text.disabled}
              textAlign={{
                xs: "center",
                md: "left",
              }}
              variant="body1"
              mt={3}
              mb={4}
            >
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Similique
              unde fugit veniam eius, perspiciatis sunt? Corporis qui ducimus
              quibusdam, aliquam dolore excepturi quae.
            </Typography>
            <Stack
              direction="row"
              justifyContent={{
                xs: "center",
                md: "flex-start",
              }}
            >
              <Button
                sx={{
                  maxWidth: "max-content",
                }}
                component={Link}
                href="/search"
              >{`Let's read!`}</Button>
            </Stack>
          </Grid>
          <Grid
            item
            xs={12}
            md={6}
            lg={7}
            display={{
              xs: "none",
              md: "block",
            }}
          >
            <Image
              src={bannerImg}
              alt="banner"
              style={{
                width: "100%",
                objectFit: "contain",
              }}
            />
          </Grid>
        </Grid>
      </Container>
    </Box>
  );
};

export default HomeBanner;
