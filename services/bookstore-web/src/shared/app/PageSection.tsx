import { ReactNode } from "react";

import { Box, Container } from "@mui/material";

type Props = {
  children: ReactNode;
  useContainer?: boolean;
};

const PageSection = (props: Props) => {
  return (
    <Box
      component={props.useContainer ? Container : undefined}
      sx={{
        py: {
          xs: 5,
          md: 8,
        },
      }}
    >
      {props.children}
    </Box>
  );
};

export default PageSection;
