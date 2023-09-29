"use client";

import Link from "next/link";

import {
  AppBar,
  Box,
  Button,
  Container,
  IconButton,
  Stack,
  Toolbar,
  useScrollTrigger,
} from "@mui/material";
import { Menu as MenuIcon } from "@mui/icons-material";

import LogoIcon from "../common/LogoIcon";
import HeaderSearchInput from "./HeaderSearchInput";
import AppSettingsDrawerButton from "./AppSettingsDrawerButton";

const Header1 = () => {
  const isScrolled = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
  });

  return (
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
                  `rgba(${theme.vars.palette.background.defaultChannel}/ 0.8)`,
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
          <Box
            href="/"
            component={Link}
            sx={{
              display: "flex",
              alignItems: "center",
            }}
          >
            <LogoIcon />
          </Box>

          <Stack alignItems="center" direction="row" spacing={0}>
            <HeaderSearchInput />
            <AppSettingsDrawerButton />
            <Button
              sx={{
                ml: 1,
                display: {
                  xs: "none",
                  md: "inline-flex",
                },
              }}
            >
              Be a member
            </Button>
            <IconButton
              edge="end"
              onClick={() => {}}
              sx={{ display: { md: "none" } }}
            >
              <MenuIcon />
            </IconButton>
          </Stack>
        </Container>
      </Toolbar>
    </AppBar>
  );
};

export default Header1;
