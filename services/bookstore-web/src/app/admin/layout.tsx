import { ReactNode } from "react";

import type { Metadata } from "next";

import { GlobalStyles } from "@mui/material";

import AdminPageLayout from "@/shared/layout/AdminPageLayout";

export const metadata: Metadata = {
  title: "ViBooks Admin",
  description: "Administration",
};

type Props = {
  children: ReactNode;
};

const AdminLayout = (props: Props) => {
  return (
    <>
      <GlobalStyles
        styles={{
          ":root": {
            "--header-height": "64px",
            "--drawer-width": "270px",
          },
        }}
      />

      <AdminPageLayout>{props.children}</AdminPageLayout>
    </>
  );
};

export default AdminLayout;
