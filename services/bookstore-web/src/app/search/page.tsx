import { Box, GlobalStyles, Stack } from "@mui/material";

import { adaptPaginationQueryParams } from "@/utils/pagination";
import PageTitle from "@/shared/app/PageTitle";
import PageLayout from "@/shared/layout/PageLayout";

import FilterSection from "./components/FilterSection";
import FilterDrawerButton from "./components/FilterDrawerButton";

const getData = async (query: PaginationQuery) => {
  const url = `https://jsonplaceholder.typicode.com/todos?_limit=${
    query.pageSize
  }&_start=${(query.page - 1) * query.pageSize} `;
  const response = await fetch(url);
  const data = response.json();

  return data;
};

const SearchPage = async (props: PageProps<PaginationQuery>) => {
  const paginationQuery = adaptPaginationQueryParams(props.searchParams);
  const data = await getData(paginationQuery);

  return (
    <PageLayout>
      <GlobalStyles
        styles={{
          ":root": {
            "--filter-section-width": "280px",
          },
        }}
      />
      <Stack
        py={5}
        direction="row"
        justifyContent="space-between"
        alignItems="center"
      >
        <PageTitle text="Books" />
        <FilterDrawerButton />
      </Stack>

      <Stack direction="row">
        <Box
          display={{
            xs: "none",
            md: "flex",
          }}
        >
          <FilterSection />
        </Box>

        <Stack
          direction="row"
          sx={{
            paddingLeft: { md: 6 },
            width: {
              xs: "100%",
              md: "calc(100% - var(--filter-section-width))",
            },
            height: 24,
            backgroundColor: "red",
          }}
        >
          
        </Stack>
      </Stack>

      {/* {data.map((item: any) => (
        <p key={item.id}>{item.id}</p>
      ))}
      <Link href={`/search?page=${paginationQuery.page + 1}`}>load more</Link> */}
    </PageLayout>
  );
};

export default SearchPage;
