import { ReactNode } from "react";

import Link, { LinkProps } from "next/link";

import { Box, BoxProps, Container, Typography } from "@mui/material";

type Props = {
  children: ReactNode;
  useContainer?: boolean;
  sx?: BoxProps["sx"];

  title?: string;
  titleHref?: LinkProps["href"];
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
          {!!props.titleHref ? (
            <Box
              component={Link}
              href={props.titleHref}
              sx={{ color: "unset" }}
            >
              <Typography variant="h3">{props.title}</Typography>
            </Box>
          ) : (
            <Typography variant="h3">{props.title}</Typography>
          )}
        </Box>
      )}
      {props.children}
    </Box>
  );
};

export default PageSection;
export type PageSectionProps = Props;
