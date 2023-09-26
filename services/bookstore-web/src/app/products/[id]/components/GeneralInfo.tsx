import { Grid, Stack, Typography } from "@mui/material";

import { Book } from "@/models";

import DetailAvatar from "./DetailAvatar";

type Props = {
  product: Book;
};

const GeneralInfo = (props: Props) => {
  const { product } = props;

  return (
    <Grid container spacing={{ xs: 4, md: 8 }}>
      <Grid item xs={12} md={5}>
        <DetailAvatar product={product} />
      </Grid>
      <Grid item xs={12} md={7}>
        <Stack spacing={2}>
          <Typography variant="h3" component="h1" fontWeight="bold">
            {product.title}
          </Typography>
          {!!product.description && (
            <Typography variant="body2" color="text.secondary">
              {product.description}
            </Typography>
          )}
        </Stack>
      </Grid>
    </Grid>
  );
};

export default GeneralInfo;
