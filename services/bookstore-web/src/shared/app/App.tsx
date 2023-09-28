"use client";

import { ReactNode } from "react";

import { RecoilRoot } from "recoil";

import ThemeRegistry from "@/theme/ThemeRegistry";

type Props = {
  children: ReactNode;
};

const App = (props: Props) => {
  return (
    <RecoilRoot>
      <ThemeRegistry>{props.children}</ThemeRegistry>
    </RecoilRoot>
  );
};

export default App;
