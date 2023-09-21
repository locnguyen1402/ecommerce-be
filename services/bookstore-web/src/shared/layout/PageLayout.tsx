import { Fragment } from "react";

import { Box, BoxProps, Container } from "@mui/material";

type Props = {
  children: React.ReactNode;
  sx?: BoxProps["sx"];
  header?: React.ReactNode;
  footer?: React.ReactNode;

  bodyWrapper?: React.PropsWithChildren<any> | false;
};

const PageLayout = (props: Props) => {
  let BodyWrapper: React.PropsWithChildren<any> = Container;

  if (props.bodyWrapper === false) {
    BodyWrapper = Fragment;
  }

  return (
    <Box
      component="div"
      sx={[
        {
          display: "flex",
          flexDirection: "column",
          height: "100%",
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      {!!props.header && props.header}

      <BodyWrapper>
        <Box component="main">{props.children}</Box>
      </BodyWrapper>

      {!!props.footer && props.footer}
    </Box>
  );
};

export default PageLayout;
