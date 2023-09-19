import { alpha, createTheme } from "@mui/material/styles";

export const lightTheme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#03beff",
    },
    grey: {
      50: "#FCFCFD",
      100: "#F9FAFB",
      200: "#F4F6F8",
      300: "#DFE3E8",
      400: "#C4CDD5",
      500: "#919EAB",
      600: "#637381",
      700: "#454F5B",
      800: "#212B36",
      900: "#161C24",
    },
    text: {
      primary: "#212B36",
      secondary: "#637381",
      disabled: "#919EAB",
    },
    divider: alpha("#919EAB", 0.24),
    background: {
      default: "#fff",
      paper: "#fff",
    },
    action: {
      active: "#637381",
      hover: alpha("#919EAB", 0.08),
      selected: alpha("#919EAB", 0.12),
      disabled: alpha("#919EAB", 0.8),
      disabledBackground: alpha("#919EAB", 0.24),
      focus: alpha("#919EAB", 0.24),
      hoverOpacity: 0.08,
      selectedOpacity: 0.08,
      disabledOpacity: 0.48,
      focusOpacity: 0.12,
      activatedOpacity: 0.12,
    },
  },
});

export const darkTheme = createTheme({
  palette: {
    mode: "dark",
    primary: {
      main: "#03beff",
    },
    grey: {
      50: "#FCFCFD",
      100: "#F9FAFB",
      200: "#F4F6F8",
      300: "#DFE3E8",
      400: "#C4CDD5",
      500: "#919EAB",
      600: "#637381",
      700: "#454F5B",
      800: "#212B36",
      900: "#161C24",
    },
    text: {
      primary: "#fff",
      secondary: "#919EAB",
      disabled: "#637381",
    },
    divider: alpha("#919EAB", 0.24),
    background: {
      default: "#161C24",
      paper: "#212B36",
    },
    action: {
      active: "#919EAB",
      hover: alpha("#919EAB", 0.08),
      selected: alpha("#919EAB", 0.12),
      disabled: alpha("#919EAB", 0.8),
      disabledBackground: alpha("#919EAB", 0.24),
      focus: alpha("#919EAB", 0.24),
      hoverOpacity: 0.08,
      selectedOpacity: 0.08,
      disabledOpacity: 0.48,
      focusOpacity: 0.12,
      activatedOpacity: 0.12,
    },
  },
});
