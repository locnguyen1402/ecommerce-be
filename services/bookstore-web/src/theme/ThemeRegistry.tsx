"use client";

import {
  CssBaseline,
  Experimental_CssVarsProvider as CssVarsProvider,
  getInitColorSchemeScript,
} from "@mui/material";

import { theme } from "./theme";

type Props = { children: React.ReactNode };

const ThemeRegistry = (props: Props) => {
  return (
    <>
      {getInitColorSchemeScript({
        defaultMode: "system",
      })}
      <CssVarsProvider theme={theme} defaultMode="system">
        <CssBaseline />
        {props.children}
      </CssVarsProvider>
    </>
  );
};

export default ThemeRegistry;
