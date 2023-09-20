"use client";

import { useState } from "react";

import {
  CssBaseline,
  ThemeProvider as MuiThemeProvider,
  PaletteMode,
  Experimental_CssVarsProvider as CssVarsProvider,
  useColorScheme,
  getInitColorSchemeScript,
} from "@mui/material";

import { ThemeContext } from "./ThemeContext";
import { getTheme, theme } from "./theme";

type Props = { children: React.ReactNode };

const ThemeRegistry = (props: Props) => {
  return (
    <>
      {getInitColorSchemeScript({
        defaultMode: "light",
      })}
      <CssVarsProvider theme={theme} defaultMode="light">
        <CssBaseline />
        {props.children}
      </CssVarsProvider>
    </>
  );
};

export default ThemeRegistry;
