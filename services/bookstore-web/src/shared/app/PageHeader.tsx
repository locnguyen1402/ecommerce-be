import { ReactNode } from "react";

import { Stack } from "@mui/material";

import PageTitle from "./PageTitle";

type Props = {
  title: string;

  action?: ReactNode;
};

const PageHeader = (props: Props) => {
  return (
    <Stack
      py={{
        xs: 2,
        md: 5,
      }}
      direction="row"
      justifyContent="space-between"
      alignItems="center"
    >
      <PageTitle text={props.title} />
      {!!props.action && props.action}
    </Stack>
  );
};

export default PageHeader;
