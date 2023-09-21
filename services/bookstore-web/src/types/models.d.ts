declare global {
  type Nullable<T> = T | null;

  type PaginationQuery = {
    page: number;
    pageSize: number;
    keyword?: string;
  };

  type PageProps<
    TSeachParams extends Record<
      string,
      number | number[] | string | string[] | undefined
    > = {},
    TParams extends Record<string, any> = {}
  > = {
    params: TParams;
    searchParams: TSeachParams;
  };
}

import "@mui/material/styles";
import "@mui/material/themeCssVarsAugmentation";

declare module "@mui/material/styles" {
  interface Palette {
    custom: Palette["primary"];
  }

  interface PaletteOptions {
    custom?: PaletteOptions["primary"];
  }
}

declare module "@mui/material/Button" {
  interface ButtonPropsColorOverrides {
    custom: true;
  }
}
