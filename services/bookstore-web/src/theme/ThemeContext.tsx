"use client";

import { createContext, useState } from "react";

import { PaletteMode } from "@mui/material";

type TContext = {
  mode: PaletteMode;
  toggleTheme: () => void;
};

export const ThemeContext = createContext<TContext>({} as TContext);
