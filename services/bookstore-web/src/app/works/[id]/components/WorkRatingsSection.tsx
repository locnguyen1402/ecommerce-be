import { Rating, Stack, Typography } from "@mui/material";

import { WorkRatings } from "@/models";

type Props = {
  ratings: WorkRatings;
};

const WorkRatingsSection = (props: Props) => {
  return (
    <Stack direction="row" alignItems="center" spacing={1}>
      <Rating value={props.ratings.average} readOnly size="large" />
      <Typography>{props.ratings.count} reviews</Typography>
    </Stack>
  );
};

export default WorkRatingsSection;
