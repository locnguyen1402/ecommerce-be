import { Skeleton, Stack } from "@mui/material";

type Props = {
  pageSize: number;
};

const SkeletonProductList = (props: Props) => {
  return (
    <>
      {Array.from(Array(props.pageSize).keys()).map((_, idx) => {
        return (
          <Stack key={idx}>
            <Skeleton
              variant="rectangular"
              width="100%"
              height={220}
              sx={{ borderRadius: 1.5 }}
            />
            <Stack p={1}>
              <Skeleton variant="text" />
              <Skeleton width="60%" />
            </Stack>
          </Stack>
        );
      })}
    </>
  );
};

export default SkeletonProductList;
