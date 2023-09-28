import { atom } from "recoil";

import { LayoutState } from "@/models";
import { PrimaryColors } from "@/theme/theme";

export const layoutStateAtom = atom<LayoutState>({
  key: "LayoutState",
  default: {
    palette: {
      primary: PrimaryColors[2],
    },
  },
});
