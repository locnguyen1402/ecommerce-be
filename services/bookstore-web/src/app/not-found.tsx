"use client";

import Image from "next/image";
import Link from "next/link";

import { Box, Button, Stack, Typography } from "@mui/material";

import PageLayout from "@/shared/layout/PageLayout";

import errorImg from "@/assets/illustration_404.svg";

const Error = () => {
  return (
    <PageLayout bodyWrapper>
      <Stack
        width="100%"
        alignItems="center"
        mt={{
          xs: 4,
          md: 6,
        }}
        spacing={{
          xs: 5,
          md: 8,
        }}
      >
        <Stack spacing={2} alignItems="center">
          <Typography variant="h3" fontWeight="bold">
            Page Not Found!
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Sorry, we couldn’t find the page you’re looking for.
            <br />
            Perhaps you’ve mistyped the URL? Be sure to check your spelling.
          </Typography>
        </Stack>

        <Box
          sx={{
            transition: "all 0.2s ease 0.1s",
            width: {
              xs: 240,
              md: 320,
            },
            height: {
              xs: 240,
              md: 320,
            },
            position: "relative",
          }}
        >
          <Image
            priority
            src={errorImg}
            alt="internalServerError"
            fill
            sizes="100%"
            style={{
              objectFit: "contain",
            }}
          />
        </Box>

        <Button component={Link} href="/" size="large">
          Go to Home
        </Button>
      </Stack>
    </PageLayout>
  );
};

export default Error;
