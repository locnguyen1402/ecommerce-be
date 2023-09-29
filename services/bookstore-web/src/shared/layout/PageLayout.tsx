import { Fragment } from "react";

import { Box, BoxProps, Container, Toolbar } from "@mui/material";

import Header1 from "../app/Header1";
import Footer from "../app/Footer";
import FloatingTopButton from "../app/FloatingTopButton";

type Props = {
  children: React.ReactNode;
  sx?: BoxProps["sx"];
  header?: Nullable<React.ReactNode>;

  bodyWrapper?: React.PropsWithChildren<any> | false;
  headerOverlap?: boolean;
};

const PageLayout = (props: Props) => {
  let Header: Nullable<React.FC> = Header1;
  let BodyWrapper: React.PropsWithChildren<any> = Container;

  if (props.bodyWrapper === false) {
    BodyWrapper = Fragment;
  }

  if (props.header === false) {
    Header = null;
  }

  return (
    <Box
      component="div"
      sx={[
        {
          display: "flex",
          flexDirection: "column",
          height: "100%",
          width: "100%",
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      {!!Header && <Header />}
      {!props.headerOverlap && <Toolbar />}

      <BodyWrapper>
        <Box
          component="main"
          sx={{
            width: "100%",
          }}
        >
          {props.children}
        </Box>
      </BodyWrapper>

      <Footer />

      <FloatingTopButton />
    </Box>
  );
};

export default PageLayout;
