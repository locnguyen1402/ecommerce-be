import { Typography, type TypographyProps } from "@mui/material";

type Props = {
  text: string;
} & TypographyProps;

const PageTitle = (props: Props) => {
  const { text, ...rest } = props;

  return (
    <Typography variant="h2" component="h2" fontWeight="bold" {...rest}>
      {text}
    </Typography>
  );
};

export default PageTitle;
