import { useSetRecoilState } from "recoil";

import { layoutStateAtom } from "../state";

export const useLayoutActions = () => {
  const setState = useSetRecoilState(layoutStateAtom);

  const changePalettePrimary = (val: string) => {
    setState((prev) => ({
      ...prev,
      palette: {
        ...prev.palette,
        primary: val,
      },
    }));
  };

  return { changePalettePrimary };
};
