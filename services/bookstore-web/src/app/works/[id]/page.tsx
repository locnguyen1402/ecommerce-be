import { Suspense } from "react";

import { ProductService } from "@/services";
import { Book } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import PageHeader from "@/shared/app/PageHeader";
import PageSection from "@/shared/app/PageSection";
import PromisesResolver from "@/shared/common/PromisesResolver";

import GeneralInfo from "./components/GeneralInfo";
import RelatedProducts from "./components/RelatedProducts";

const ProductDetailPage = (
  props: PageProps<{ bookId?: string }, { id: string }>
) => {
  const workDetailPromise = ProductService.getWorkDetails(props.params.id);
  let bookDetailPromise: Promise<SuccessResponse<Nullable<Book>>>;

  if (!!props.searchParams.bookId) {
    bookDetailPromise = ProductService.getBookDetails(
      props.searchParams.bookId
    );
  } else {
    bookDetailPromise = ProductService.getFirstInWorkBook(props.params.id);
  }

  return (
    <PageLayout>
      <PageHeader title="Details" />

      <div key={Math.random().toString()}>
        <Suspense fallback={"loading..."}>
          <PromisesResolver promises={[workDetailPromise, bookDetailPromise]}>
            {([workVal, bookVal]) => (
              <GeneralInfo
                workDetail={workVal!.data}
                bookDetail={bookVal?.data || null}
              />
            )}
          </PromisesResolver>
        </Suspense>
      </div>

      <PageSection>
        <RelatedProducts />
      </PageSection>
    </PageLayout>
  );
};

export default ProductDetailPage;
