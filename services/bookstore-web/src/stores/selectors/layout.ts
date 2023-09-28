import { selector } from "recoil";

import { layoutStateAtom } from "../state";

export const layoutStateSelector = selector({
  key: "layoutStateSelector",
  get: ({ get }) => {
    return get(layoutStateAtom);
  },
});
