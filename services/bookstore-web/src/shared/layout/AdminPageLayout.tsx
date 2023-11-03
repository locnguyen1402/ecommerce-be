import { Box } from "@mui/material";
import { ReactNode } from "react";

type Props = {
  children: ReactNode;
};

const AdminPageLayout = (props: Props) => {
  return (
    <Box component="div">
      <Box
        sx={{
          width: "100%",
          height: "var(--header-height)",
          position: "fixed",
          top: 0,
          left: 0,
          backgroundColor: "green",
        }}
      />

      <Box
        sx={{
          width: {
            xs: 0,
            md: "var(--drawer-width)",
          },
          height: "calc(100vh - var(--header-height))",
          position: "fixed",
          top: "var(--header-height)",
          left: 0,
          transition: "width .2s ease-in-out",
          backgroundColor: "blue",
        }}
      />

      <Box
        sx={{
          width: "100%",
          height: "100%",
          paddingTop: "var(--header-height)",
          paddingLeft: {
            xs: 0,
            md: "var(--drawer-width)",
          },
          transition: "padding-left .2s ease-in-out",
        }}
      >
        test
        {props.children}
      </Box>
    </Box>
  );
};

export default AdminPageLayout;
