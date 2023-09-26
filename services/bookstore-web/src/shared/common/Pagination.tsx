"use client";

import { useMemo } from "react";

import Link from "next/link";
import { usePathname, useSearchParams } from "next/navigation";

import { PaginationItem, Pagination as MuiPagination } from "@mui/material";

type Props = {
  info: PaginationInfo;
};

const Pagination = (props: Props) => {
  const { info } = props;
  const pathname = usePathname();
  const searchParams = useSearchParams();

  const queryParams = useMemo(() => {
    return new URLSearchParams(searchParams);
  }, [searchParams]);

  return (
    <MuiPagination
      color="primary"
      count={info.totalPages}
      page={info.page}
      renderItem={(item) => {
        if (!!item.page) {
          queryParams.set("page", item.page.toString());
        }
        const url = `${pathname}?${queryParams.toString()}`;

        item.onClick;
        return (
          <PaginationItem
            component={Link}
            href={url}
            scroll={false}
            {...item}
            onClick={(evt) => {
              window.scrollTo({
                top: 0,
                behavior: "smooth",
              });

              !!item.onClick && item.onClick(evt);
            }}
          />
        );
      }}
    />
  );
};

export default Pagination;
