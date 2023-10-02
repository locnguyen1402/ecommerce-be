import { Box, Button, Grid, Stack, Typography } from "@mui/material";
import { AddShoppingCartOutlined as AddShoppingCartIcon } from "@mui/icons-material";

import { Work, Book, WorkRatings } from "@/models";

import ProductAvatar from "@/shared/common/ProductAvatar";
import WorkRatingsSection from "./WorkRatingsSection";

type Props = {
  workDetail: Work;
  workRatings: WorkRatings;
  bookDetail: Nullable<Book>;
};

const GeneralInfo = (props: Props) => {
  const { workDetail, bookDetail, workRatings } = props;

  return (
    <Grid container spacing={{ xs: 4, md: 8 }}>
      <Grid item xs={12} md={5}>
        <Box
          sx={{
            height: {
              xs: 300,
              sm: 360,
              md: 420,
            },
          }}
        >
          <ProductAvatar
            src={workDetail.imageUrlM}
            alt={workDetail.title}
            sx={{
              height: "100%",
              maxHeight: "100%",
            }}
          />
        </Box>
      </Grid>
      <Grid item xs={12} md={7}>
        <Typography variant="h3" component="h1" fontWeight="bold">
          {bookDetail?.title || workDetail.title}
        </Typography>
        <Stack
          mt={{
            xs: 2,
            md: 4,
          }}
          spacing={2}
        >
          <WorkRatingsSection ratings={workRatings} />
          {!!workDetail.description && (
            <Typography variant="body2" color="text.secondary">
              {workDetail.description}
            </Typography>
          )}

          <Stack direction="row" spacing={2}>
            <Button
              size="large"
              sx={{
                textTransform: "capitalize",
                width: {
                  xs: "100%",
                  md: "max-content",
                },
              }}
              startIcon={<AddShoppingCartIcon />}
            >
              Add to Card
            </Button>
            <Button
              size="large"
              sx={{
                textTransform: "capitalize",
                width: {
                  xs: "100%",
                  md: "max-content",
                },
              }}
              color="primary"
            >
              Buy now
            </Button>
          </Stack>
        </Stack>
      </Grid>
    </Grid>
  );
};

export default GeneralInfo;
