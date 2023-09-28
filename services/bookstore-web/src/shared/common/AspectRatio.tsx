import { ReactNode } from "react";

import { Box, BoxProps } from "@mui/material";

type Props = {
  ratio?: number;
  children: ReactNode;

  sx?: BoxProps["sx"];
};

const AspectRatio = (props: Props) => {
  return (
    <Box
      component="div"
      position="relative"
      sx={[
        {
          width: "100%",
          height: "100%",
        },
        ...(Array.isArray(props.sx) ? props.sx : [props.sx]),
      ]}
    >
      <Box
        component="div"
        sx={{
          position: "absolute",
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          "& > *": { height: "100%", width: "100%" },
        }}
      >
        {props.children}
      </Box>
      <Box
        component="div"
        style={{ paddingBottom: (1 / (props.ratio || 1)) * 100 + "%" }}
      />
    </Box>
  );
};

export default AspectRatio;
