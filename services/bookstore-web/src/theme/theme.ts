import { PaletteMode, PaletteOptions } from "@mui/material";
import {
  Components,
  alpha,
  createTheme,
  Theme,
  experimental_extendTheme as extendTheme,
} from "@mui/material/styles";

const { palette } = createTheme();
const { augmentColor } = palette;

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

const componentOptions: Components<Omit<Theme, "components">> = {
  MuiButton: {
    styleOverrides: {
      root: () => ({
        fontWeight: "bold",
      }),
    },
    defaultProps: {
      variant: "contained",
      color: "custom",
    },
  },
};

const lightPalette: PaletteOptions = {
  mode: "light",
  primary: {
    main: "#03beff",
  },
  grey: GreyPalette,
  custom: augmentColor({ color: { main: GreyPalette[800] } }),
  text: {
    primary: GreyPalette[800],
    secondary: GreyPalette[600],
    disabled: GreyPalette[500],
  },
  divider: alpha(GreyPalette[500], 0.24),
  background: {
    // default: "#fff",
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
};

const darkPalette: PaletteOptions = {
  mode: "dark",
  primary: {
    main: "#03beff",
  },
  grey: GreyPalette,
  custom: augmentColor({ color: { main: "#fff" } }),
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
};

export const getTheme = (mode: PaletteMode) => {
  return createTheme({
    palette: mode === "light" ? lightPalette : darkPalette,
    components: componentOptions,
  });
};

export const theme = extendTheme({
  colorSchemes: {
    light: {
      palette: lightPalette,
    },
    dark: {
      palette: darkPalette,
    },
  },
  components: componentOptions,
});
