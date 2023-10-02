import { ProductService } from "@/services";
import { Book, TrendingType } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import PageHeader from "@/shared/app/PageHeader";
import ProductsCarouselSection from "@/shared/app/ProductsCarouselSection";
import TrendingProductsCarouselSection from "@/shared/app/TrendingProductsCarouselSection";

import GeneralInfo from "./components/GeneralInfo";

const ProductDetailPage = async (
  props: PageProps<{ bookId?: string }, { id: string }>
) => {
  const workDetail = await ProductService.getWorkDetails(props.params.id);
  const workRatings = await ProductService.getWorkRatings(props.params.id);
  let bookDetail: SuccessResponse<Nullable<Book>>;

  if (!!props.searchParams.bookId) {
    bookDetail = await ProductService.getBookDetails(props.searchParams.bookId);
  } else {
    bookDetail = await ProductService.getFirstInWorkBook(props.params.id);
  }

  const subjectRelatedKeyword = workDetail.data.subjects
    ?.map((item) => `"${item}"`)
    ?.join("OR");

  let keyword = !!subjectRelatedKeyword
    ? `subject:(${subjectRelatedKeyword})`
    : undefined;

  return (
    <PageLayout>
      <PageHeader title="Details" />

      <GeneralInfo
        workDetail={workDetail.data}
        workRatings={workRatings.data}
        bookDetail={bookDetail?.data || null}
      />

      <ProductsCarouselSection
        query={{
          keyword,
        }}
        pageSectionProps={{
          title: "You might also like",
        }}
      />

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
    </PageLayout>
  );
};

export default ProductDetailPage;
