"use client";

import { useState } from "react";

import {
  Box,
  Button,
  ClickAwayListener,
  Container,
  IconButton,
  InputAdornment,
  Slide,
  TextField,
  alpha,
} from "@mui/material";
import { SearchOutlined as SearchIcon } from "@mui/icons-material";

const HeaderSearchInput = () => {
  const [visible, setVisible] = useState<boolean>(false);

  const openSearchBar = () => setVisible(true);

  const closeSearchBar = () => setVisible(false);

  return (
    <>
      <IconButton onClick={openSearchBar}>
        <SearchIcon />
      </IconButton>
      <ClickAwayListener
        mouseEvent="onMouseDown"
        touchEvent="onTouchStart"
        onClickAway={closeSearchBar}
      >
        <Slide direction="down" in={visible} mountOnEnter unmountOnExit>
          <Box
            sx={{
              position: "fixed",
              top: 0,
              width: "100%",
              left: 0,
              zIndex: (theme) => theme.zIndex.appBar + 1,
              height: 80,
              display: "flex",
              alignItems: "center",
              backgroundColor: (theme) =>
                `rgba(${theme.vars.palette.background.default}, 0.72)`,
              backdropFilter: "blur(6px)",
            }}
          >
            <Container
              sx={{
                display: "flex",
                alignItems: "center",
              }}
            >
              <TextField
                autoFocus
                fullWidth
                variant="standard"
                placeholder="Search"
                autoComplete="off"
                InputProps={{
                  disableUnderline: true,
                  startAdornment: (
                    <InputAdornment position="start">
                      <SearchIcon />
                    </InputAdornment>
                  ),
                }}
              />
              <Button sx={{ ml: 1 }} onClick={closeSearchBar}>
                Search
              </Button>
            </Container>
          </Box>
        </Slide>
      </ClickAwayListener>
    </>
  );
};

export default HeaderSearchInput;
