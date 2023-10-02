import { Container, Typography } from "@mui/material";

import { TrendingType } from "@/models";

import PageLayout from "@/shared/layout/PageLayout";
import TrendingProductsCarouselSection from "@/shared/app/TrendingProductsCarouselSection";

import HomeBanner from "./components/HomeBanner";

const HomePage = () => {
  return (
    <PageLayout headerOverlap bodyWrapper={false}>
      <HomeBanner />
      <Container>
        <TrendingProductsCarouselSection
          query={{
            type: TrendingType.DAILY,
          }}
          pageSectionProps={{
            title: "Trending: Today",
          }}
        />

        <TrendingProductsCarouselSection
          query={{
            type: TrendingType.FOREVER,
          }}
          pageSectionProps={{
            title: "Trending: All Time",
          }}
        />
      </Container>
    </PageLayout>
  );
};

export default HomePage;
