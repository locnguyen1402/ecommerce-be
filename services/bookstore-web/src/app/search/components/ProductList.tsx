import { Box } from "@mui/material";

import { SearchProductItem } from "@/models";
import Pagination from "@/shared/common/Pagination";

import ProductCard from "./ProductCard";

type Props = SuccessResponse<SearchProductItem[]>;

const ProductList = (props: Props) => {
  const { data, meta } = props;
  return (
    <>
      {!!data.length && (
        <>
          {data.map((item) => {
            return <ProductCard key={item.id} product={item} />;
          })}

          {!!meta?.pagination && (
            <Box
              gridColumn={{
                xs: "span 2",
                sm: "span 3",
                lg: "span 4",
              }}
              display="flex"
              justifyContent="center"
              mt={{
                xs: 2,
                md: 4,
              }}
            >
              <Pagination info={meta?.pagination} />
            </Box>
          )}
        </>
      )}
    </>
  );
};

export default ProductList;
