import { Suspense } from "react";

import { ProductService } from "@/services";

import PromiseResolver from "@/shared/common/PromiseResolver";
import PageLayout from "@/shared/layout/PageLayout";

import GeneralInfo from "./components/GeneralInfo";

const ProductDetailPage = (props: PageProps<{}, { id: string }>) => {
  const detailPromise = ProductService.getDetails(props.params.id);

  return (
    <PageLayout>
      <Suspense fallback={"loading..."}>
        <PromiseResolver promise={detailPromise}>
          {(val) => <GeneralInfo product={val.data} />}
        </PromiseResolver>
      </Suspense>
    </PageLayout>
  );
};

export default ProductDetailPage;
