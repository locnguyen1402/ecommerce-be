"use client";

import { ReactNode, useState } from "react";

import { RecoilRoot } from "recoil";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import ThemeRegistry from "@/theme/ThemeRegistry";

type Props = {
  children: ReactNode;
};

const App = (props: Props) => {
  const [queryClient] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: {
            retry: 0,
            refetchOnWindowFocus: false,
          },
        },
      })
  );
  return (
    <QueryClientProvider client={queryClient}>
      <RecoilRoot>
        <ThemeRegistry>{props.children}</ThemeRegistry>
      </RecoilRoot>
    </QueryClientProvider>
  );
};

export default App;
