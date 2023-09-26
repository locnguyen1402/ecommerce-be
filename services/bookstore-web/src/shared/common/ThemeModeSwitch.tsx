"use client";

import { useEffect, useState } from "react";

import { useColorScheme } from "@mui/material";

import StyledThemeModeSwitch from "./StyledThemeModeSwitch";

const ThemeModeSwitch = () => {
  const { mode, setMode } = useColorScheme();
  const [mounted, setMounted] = useState(false);

  const toggleTheme = () => {
    setMode(mode === "light" ? "dark" : "light");
  };

  useEffect(() => {
    setMounted(true);
  }, []);

  if (!mounted) {
    return <></>;
  }

  return (
    <StyledThemeModeSwitch checked={mode === "light"} onChange={toggleTheme} />
  );
};

export default ThemeModeSwitch;
