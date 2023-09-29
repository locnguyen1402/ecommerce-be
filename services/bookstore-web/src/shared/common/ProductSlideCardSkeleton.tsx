import { Box, Paper, Skeleton, alpha, useTheme } from "@mui/material";

import AspectRatio from "./AspectRatio";

const ProductSlideCardSkeleton = () => {
  const theme = useTheme();

  return (
    <Box
      py={0.5}
      px={{
        xs: 1,
        md: 1.5,
      }}
    >
      <Paper
        variant="outlined"
        sx={{
          p: 1.5,
          borderRadius: 4,
          ":hover": {
            backgroundColor: "grey.200",
            [theme.getColorSchemeSelector("dark")]: {
              backgroundColor: alpha(theme.palette.grey[500], 0.12),
            },
          },
        }}
      >
        <Box
          sx={{
            mb: {
              xs: 1,
              md: 2,
            },
          }}
        >
          <AspectRatio ratio={12 / 16}>
            <Skeleton variant="rounded" width="100%" height="100%" />
          </AspectRatio>
        </Box>
        <Skeleton variant="text" />
      </Paper>
    </Box>
  );
};

export default ProductSlideCardSkeleton;
