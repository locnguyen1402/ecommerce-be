"use client";

import {
  Backdrop,
  Box,
  BoxProps,
  CircularProgress,
  useTheme,
} from "@mui/material";

import LogoIcon from "./LogoIcon";

type Props = {
  sx?: BoxProps["sx"];
  logoSx?: BoxProps["sx"];
};

const BackdropLoading = (props: Props) => {
  const theme = useTheme();

  return (
    <Backdrop
      sx={{
        color: "#fff",
        zIndex: (theme) => theme.zIndex.drawer + 1,
        userSelect: "none",
      }}
      open
    >
      <Box
        sx={{
          position: "relative",
          width: {
            xs: 120,
            md: 160,
          },
          height: {
            xs: 120,
            md: 160,
          },
        }}
      >
        <CircularProgress
          sx={{
            width: "100% !important",
            height: "100% !important",
          }}
        />
        <Box
          sx={[
            {
              "@keyframes effectExit": {
                "0%": {
                  opacity: 0.2,
                },
                "100%": {
                  opacity: 1,
                },
              },
              animation: `effectExit 2s ${theme.transitions.easing.easeIn} infinite`,
              top: 0,
              left: 0,
              bottom: 0,
              right: 0,
              position: "absolute",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            },
          ]}
        >
          <LogoIcon
            sx={{
              width: "75%",
            }}
          />
        </Box>
      </Box>
    </Backdrop>
  );
};

export default BackdropLoading;
