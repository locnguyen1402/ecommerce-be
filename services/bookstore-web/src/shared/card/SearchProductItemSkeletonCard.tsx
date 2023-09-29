import { Stack, Skeleton } from "@mui/material";

const SearchProductItemSkeletonCard = () => {
  return (
    <Stack>
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
};

export default SearchProductItemSkeletonCard;
