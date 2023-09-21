import { PaletteOptions } from "@mui/material";
import {
  Components,
  alpha,
  createTheme,
  Theme,
  experimental_extendTheme as extendTheme,
  responsiveFontSizes,
} from "@mui/material/styles";

const {
  palette,
  typography: { pxToRem },
  breakpoints,
} = createTheme();
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

let temp = extendTheme({
  cssVarPrefix: "vibooks",
  colorSchemes: {
    light: {
      palette: lightPalette,
    },
    dark: {
      palette: darkPalette,
    },
  },
  components: componentOptions,
  typography: {
    h1: {
      fontSize: pxToRem(40),
      [breakpoints.up("sm")]: {
        fontSize: pxToRem(52),
      },
      [breakpoints.up("md")]: {
        fontSize: pxToRem(58),
      },
      [breakpoints.up("lg")]: {
        fontSize: pxToRem(64),
      },
    },
    h2: {
      fontSize: pxToRem(32),
      [breakpoints.up("sm")]: {
        fontSize: pxToRem(40),
      },
      [breakpoints.up("md")]: {
        fontSize: pxToRem(44),
      },
      [breakpoints.up("lg")]: {
        fontSize: pxToRem(48),
      },
    },
    h3: {
      fontSize: pxToRem(24),
      [breakpoints.up("sm")]: {
        fontSize: pxToRem(28),
      },
      [breakpoints.up("md")]: {
        fontSize: pxToRem(30),
      },
      [breakpoints.up("lg")]: {
        fontSize: pxToRem(32),
      },
    },
    h4: {
      fontSize: pxToRem(20),
      [breakpoints.up("sm")]: {
        fontSize: pxToRem(22),
      },
      [breakpoints.up("md")]: {
        fontSize: pxToRem(23),
      },
      [breakpoints.up("lg")]: {
        fontSize: pxToRem(24),
      },
    },
    h5: {
      fontSize: pxToRem(18),
      [breakpoints.up("sm")]: {
        fontSize: pxToRem(18),
      },
      [breakpoints.up("md")]: {
        fontSize: pxToRem(18),
      },
      [breakpoints.up("lg")]: {
        fontSize: pxToRem(20),
      },
    },
    h6: {
      fontSize: pxToRem(18),
    },
    subtitle2: {
      fontSize: pxToRem(14),
    },
    body2: {
      fontSize: pxToRem(14),
    },
    caption: {
      fontSize: pxToRem(12),
    },
    overline: {
      fontSize: pxToRem(12),
    },
  },
});

export const theme = temp;
