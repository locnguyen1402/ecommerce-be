import { Box, BoxProps, Container } from "@mui/material";

type Props = {
  children: React.ReactNode;
  sx?: BoxProps["sx"];
  header?: React.ReactNode;
  footer?: React.ReactNode;
};

const PageLayout = (props: Props) => {
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

      <Container>
        <Box component="main">{props.children}</Box>
      </Container>

      {!!props.footer && props.footer}
    </Box>
  );
};

export default PageLayout;
