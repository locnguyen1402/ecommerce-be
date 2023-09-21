"use client";

import { useRef, useState } from "react";

import { useRouter } from "next/navigation";

import {
  Box,
  Button,
  ClickAwayListener,
  Container,
  IconButton,
  InputAdornment,
  Slide,
  TextField,
} from "@mui/material";
import { SearchOutlined as SearchIcon } from "@mui/icons-material";

const HeaderSearchInput = () => {
  const router = useRouter();
  const [visible, setVisible] = useState<boolean>(false);
  const inputRef = useRef<HTMLInputElement>(null);

  const openSearchBar = () => setVisible(true);

  const closeSearchBar = () => setVisible(false);

  const onSearch = () => {
    const keyword = inputRef.current?.value;

    closeSearchBar();
    router.push(`/search?keyword=${keyword}`);
  };

  return (
    <>
      <IconButton onClick={openSearchBar}>
        <SearchIcon />
      </IconButton>
      <div>
        <ClickAwayListener
          mouseEvent="onMouseDown"
          touchEvent="onTouchStart"
          onClickAway={closeSearchBar}
        >
          <Slide direction="down" in={visible} mountOnEnter unmountOnExit>
            <Box
              sx={{
                position: "absolute",
                top: 0,
                width: "100%",
                left: 0,
                zIndex: 1,
                height: 80,
                display: "flex",
                alignItems: "center",
                backgroundColor: (theme) =>
                  `rgba(${theme.vars.palette.background.defaultChannel}, 0.8)`,
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
                  inputRef={inputRef}
                />
                <Button sx={{ ml: 1 }} onClick={onSearch}>
                  Search
                </Button>
              </Container>
            </Box>
          </Slide>
        </ClickAwayListener>
      </div>
    </>
  );
};

export default HeaderSearchInput;
