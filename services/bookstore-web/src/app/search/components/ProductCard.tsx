import { SearchProductItem } from "@/models/search";
import { Box, Stack, Typography } from "@mui/material";
import Image from "next/image";

type Props = {
  product: SearchProductItem;
};

const ProductCard = (props: Props) => {
  const { product } = props;
  return (
    <Stack>
      <Box
        sx={{
          width: '100%',
          overflow: "hidden",
        }}
      >
        {!!product.coverImageUrl ? (
          <Image
            src={product.coverImageUrl}
            alt={product.title}
            fill
            style={{
              objectFit: "contain",
            }}
          />
        ) : (
          <></>
        )}
      </Box>
      <Typography>{product.title}</Typography>
    </Stack>
  );
};

export default ProductCard;
