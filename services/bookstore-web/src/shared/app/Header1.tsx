"use client";

import {
  AppBar,
  Box,
  Button,
  Container,
  IconButton,
  Stack,
  Toolbar,
  alpha,
  useScrollTrigger,
} from "@mui/material";
import { Menu as MenuIcon } from "@mui/icons-material";

import LogoIcon from "../common/LogoIcon";
import ThemeModeSwitch from "../common/ThemeModeSwitch";
import HeaderSearchInput from "./HeaderSearchInput";

const Header1 = () => {
  const isScrolled = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
  });

  return (
    <>
      <AppBar
        color="inherit"
        sx={{ backgroundColor: "transparent" }}
        elevation={0}
        position="fixed"
      >
        <Toolbar
          disableGutters
          sx={{
            transition:
              "height 0.2s cubic-bezier(0.4, 0, 0.2, 1) 0s, background-color 0.2s cubic-bezier(0.4, 0, 0.2, 1) 0s",
            height: {
              xs: 56,
              sm: 64,
              md: 80,
            },
            ...(isScrolled
              ? {
                  backdropFilter: "blur(6px)",
                  backgroundColor: (theme) =>
                    `rgba(${theme.vars.palette.background.default}, 0.8)`,
                  height: {
                    xs: 64,
                  },
                }
              : {}),
          }}
        >
          <Container
            sx={{
              height: "100%",
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
            }}
          >
            <LogoIcon />

            <Stack
              display={{
                md: "none",
              }}
              direction="row"
              spacing={1}
            >
              <HeaderSearchInput />
              <ThemeModeSwitch />
            </Stack>

            <Stack
              display={{
                xs: "none",
                md: "flex",
              }}
              alignItems="center"
              direction="row"
              spacing={1}
            >
              <HeaderSearchInput />
              <ThemeModeSwitch />
              <Button>Be a member</Button>
            </Stack>
            <IconButton
              edge="end"
              onClick={() => {}}
              sx={{ display: { md: "none" } }}
            >
              <MenuIcon />
            </IconButton>
          </Container>
        </Toolbar>
      </AppBar>

      {/* <Toolbar /> */}
    </>
  );
};

export default Header1;
