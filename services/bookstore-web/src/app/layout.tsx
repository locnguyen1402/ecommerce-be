import type { Metadata } from "next";

import App from "@/shared/app/App";

export const metadata: Metadata = {
  title: "Vi Books",
  description: "Knowledge is power",
};

type Props = {
  children: React.ReactNode;
};

const RootLayout = (props: Props) => {
  return (
    <html
      lang="en"
      style={{
        scrollBehavior: "smooth",
      }}
      suppressHydrationWarning
    >
      <body>
        <App>{props.children}</App>
      </body>
    </html>
  );
};

export default RootLayout;
