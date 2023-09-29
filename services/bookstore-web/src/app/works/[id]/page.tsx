import { Suspense } from "react";

import { ProductService } from "@/services";
import { Book } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import PageHeader from "@/shared/app/PageHeader";
import PromisesResolver from "@/shared/common/PromisesResolver";
import BackdropLoading from "@/shared/common/BackdropLoading";
import QueryExecutor from "@/shared/common/QueryExecutor";

import GeneralInfo from "./components/GeneralInfo";
import ProductsCarousel from "@/shared/common/ProductsCarousel";
import ProductSlideCard from "@/shared/card/ProductSlideCard";

const ProductDetailPage = (
  props: PageProps<{ bookId?: string }, { id: string }>
) => {
  const workDetailPromise = ProductService.getWorkDetails(props.params.id);
  const workRatingsPromise = ProductService.getWorkRatings(props.params.id);
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
        <Suspense fallback={<BackdropLoading />}>
          <PromisesResolver
            promises={[
              workDetailPromise,
              workRatingsPromise,
              bookDetailPromise,
            ]}
          >
            {([workVal, workRatings, bookVal]) => {
              const subjectRelatedKeyword = workVal.data.subjects
                ?.map((item) => `"${item}"`)
                ?.join("OR");

              let keyword = !!subjectRelatedKeyword
                ? `subject:(${subjectRelatedKeyword})`
                : undefined;

              return (
                <>
                  <GeneralInfo
                    workDetail={workVal!.data}
                    workRatings={workRatings.data}
                    bookDetail={bookVal?.data || null}
                  />

                  {/* "You might also like" */}
                  <QueryExecutor
                    queryPromise={() =>
                      ProductService.searchProducts({
                        page: 1,
                        pageSize: 20,
                        keyword,
                      })
                    }
                  >
                    {({ isLoading, data }) => {
                      return <></>;
                      // <ProductsCarousel
                      //     isLoading={isLoading}
                      //     data={data}
                      //     itemRender={(item) => {
                      //       return (
                      //         <ProductSlideCard
                      //           imgSrc={item.coverImageUrl}
                      //           title={item.title}
                      //           href={{
                      //             pathname: `/works/${item.id}`,
                      //             query: {
                      //               bookId: item.firstEditionId,
                      //             },
                      //           }}
                      //         />
                      //       );
                      //     }}
                      //   />
                    }}
                  </QueryExecutor>
                </>
              );
            }}
          </PromisesResolver>
        </Suspense>
      </div>
    </PageLayout>
  );
};

export default ProductDetailPage;
