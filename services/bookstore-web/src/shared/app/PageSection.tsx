import { ReactNode } from "react";

import { Box, BoxProps, Container, Typography } from "@mui/material";

type Props = {
  children: ReactNode;
  useContainer?: boolean;
  title?: string;
  sx?: BoxProps["sx"];
};

const PageSection = (props: Props) => {
  return (
    <Box
      component={props.useContainer ? Container : undefined}
      sx={[
        {
          py: {
            xs: 5,
            md: 8,
          },
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      {!!props.title && (
        <Box
          sx={{
            display: "flex",
            mb: {
              xs: 2,
              md: 4,
            },
          }}
        >
          <Typography variant="h3">{props.title}</Typography>
        </Box>
      )}
      {props.children}
    </Box>
  );
};

export default PageSection;
export type PageSectionProps = Props;
