"use client";

import { useContext } from "react";

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
import {
  Menu as MenuIcon,
  SearchOutlined as SearchIcon,
  SettingsOutlined as SettingsIcon,
} from "@mui/icons-material";

import { ThemeContext } from "@/theme/ThemeContext";
import LogoIcon from "../common/LogoIcon";

const Header1 = () => {
  const { toggleTheme } = useContext(ThemeContext);
  const isScrolled = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
  });

  return (
    <>
      <AppBar
        color="default"
        sx={{ backgroundColor: "transparent" }}
        elevation={0}
        position="fixed"
      >
        <Toolbar
          disableGutters
          sx={
            isScrolled
              ? {
                  backdropFilter: "blur(6px)",
                  backgroundColor: (theme) =>
                    alpha(theme.palette.background.default, 0.8),
                }
              : undefined
          }
        >
          <Container
            maxWidth="lg"
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
              <IconButton onClick={() => {}}>
                <SearchIcon />
              </IconButton>
              <IconButton onClick={toggleTheme}>
                <SettingsIcon />
              </IconButton>
            </Stack>

            <Stack
              display={{
                xs: "none",
                md: "flex",
              }}
              direction="row"
              spacing={1}
            >
              <IconButton onClick={() => {}}>
                <SearchIcon />
              </IconButton>
              <IconButton onClick={() => {}}>
                <SettingsIcon />
              </IconButton>
              <Button variant="contained">Be a member</Button>
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

      <Toolbar />
    </>
  );
};

export default Header1;
