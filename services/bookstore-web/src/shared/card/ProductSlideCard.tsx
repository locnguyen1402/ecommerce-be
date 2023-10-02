import Link, { LinkProps } from "next/link";

import { Box, Paper, Typography, alpha, useTheme } from "@mui/material";

import ProductAvatar from "../common/ProductAvatar";

type Props = {
  imgSrc: string | undefined;
  title: string;

  href?: LinkProps["href"];
};

const ProductSlideCard = (props: Props) => {
  const theme = useTheme();

  return (
    <Box
      py={0.5}
      px={{
        xs: 1,
        md: 1.5,
      }}
    >
      <Box
        component={(!!props.href ? Link : undefined) as any}
        href={props.href}
        sx={{
          textDecoration: "none",
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
          <ProductAvatar
            src={props.imgSrc}
            alt={props.title}
            sx={{
              mb: {
                xs: 1,
                md: 2,
              },
            }}
          />
          <Typography
            variant="body2"
            component="p"
            color="text.primary"
            fontWeight="bold"
            sx={{
              textOverflow: "ellipsis",
              overflow: "hidden",
              whiteSpace: "nowrap",
            }}
          >
            {props.title}
          </Typography>
        </Paper>
      </Box>
    </Box>
  );
};

export default ProductSlideCard;
