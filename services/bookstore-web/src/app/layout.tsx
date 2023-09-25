import type { Metadata } from "next";

import ThemeRegistry from "@/theme/ThemeRegistry";

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
    >
      <body>
        <ThemeRegistry>{props.children}</ThemeRegistry>
      </body>
    </html>
  );
};

export default RootLayout;
