import { Suspense } from "react";

import { ProductService } from "@/services";
import { Book, TrendingType } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import PageHeader from "@/shared/app/PageHeader";
import PromisesResolver from "@/shared/common/PromisesResolver";
import BackdropLoading from "@/shared/common/BackdropLoading";
import ProductsCarouselSection from "@/shared/app/ProductsCarouselSection";
import TrendingProductsCarouselSection from "@/shared/app/TrendingProductsCarouselSection";

import GeneralInfo from "./components/GeneralInfo";

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

      <div key={props.params.id}>
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

                  <ProductsCarouselSection
                    query={{
                      keyword,
                    }}
                    pageSectionProps={{
                      title: "You might also like",
                    }}
                  />
                </>
              );
            }}
          </PromisesResolver>
        </Suspense>

        <TrendingProductsCarouselSection
          query={{
            type: TrendingType.WEEKLY,
          }}
          pageSectionProps={{
            title: "Trending: This week",
            titleHref: {
              pathname: `/trending/${TrendingType.WEEKLY}`,
            },
          }}
        />
      </div>
    </PageLayout>
  );
};

export default ProductDetailPage;
