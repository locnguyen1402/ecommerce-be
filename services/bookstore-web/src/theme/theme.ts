import { alpha, createTheme } from "@mui/material/styles";

const GreyPalette = {
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
};

export const lightTheme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#03beff",
    },
    grey: GreyPalette,
    text: {
      primary: GreyPalette[800],
      secondary: GreyPalette[600],
      disabled: GreyPalette[500],
    },
    divider: alpha(GreyPalette[500], 0.24),
    background: {
      default: "#fff",
      paper: "#fff",
    },
    action: {
      active: GreyPalette[600],
      hover: alpha(GreyPalette[500], 0.08),
      selected: alpha(GreyPalette[500], 0.12),
      disabled: alpha(GreyPalette[500], 0.8),
      disabledBackground: alpha(GreyPalette[500], 0.24),
      focus: alpha(GreyPalette[500], 0.24),
      hoverOpacity: 0.08,
      selectedOpacity: 0.08,
      disabledOpacity: 0.48,
      focusOpacity: 0.12,
      activatedOpacity: 0.12,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: ({ ownerState }) => ({
          fontWeight: "bold",
          ...(ownerState.variant === "contained" &&
            ownerState.color === "primary" && {
              backgroundColor: GreyPalette[800],
              color: "#fff",
            }),
        }),
      },
    },
  },
});

export const darkTheme = createTheme({
  palette: {
    mode: "dark",
    primary: {
      main: "#03beff",
    },
    grey: GreyPalette,
    text: {
      primary: "#fff",
      secondary: GreyPalette[500],
      disabled: GreyPalette[600],
    },
    divider: alpha(GreyPalette[500], 0.24),
    background: {
      default: GreyPalette[900],
      paper: GreyPalette[800],
    },
    action: {
      active: GreyPalette[500],
      hover: alpha(GreyPalette[500], 0.08),
      selected: alpha(GreyPalette[500], 0.12),
      disabled: alpha(GreyPalette[500], 0.8),
      disabledBackground: alpha(GreyPalette[500], 0.24),
      focus: alpha(GreyPalette[500], 0.24),
      hoverOpacity: 0.08,
      selectedOpacity: 0.08,
      disabledOpacity: 0.48,
      focusOpacity: 0.12,
      activatedOpacity: 0.12,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: ({ ownerState }) => ({
          fontWeight: "bold",
          ...(ownerState.variant === "contained" &&
            ownerState.color === "primary" && {
              backgroundColor: "#fff",
              color: GreyPalette[800],
            }),
        }),
      },
    },
  },
});
