"use client";

import { useState } from "react";

import {
  CssBaseline,
  ThemeProvider as MuiThemeProvider,
  PaletteMode,
} from "@mui/material";

import { ThemeContext } from "./ThemeContext";
import { lightTheme, darkTheme } from "./theme";

type Props = { children: React.ReactNode };

const ThemeRegistry = (props: Props) => {
  const [mode, setMode] = useState<PaletteMode>("light");

  const toggle = () => {
    setMode((prev) => (prev === "dark" ? "light" : "dark"));
  };

  return (
    <ThemeContext.Provider value={{ toggleTheme: toggle, mode }}>
      <MuiThemeProvider theme={mode === "light" ? lightTheme : darkTheme}>
        <CssBaseline />
        {props.children}
      </MuiThemeProvider>
    </ThemeContext.Provider>
  );
};

export default ThemeRegistry;
