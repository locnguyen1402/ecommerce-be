"use client";

import { useEffect, useMemo } from "react";

import Link from "next/link";
import { usePathname, useRouter, useSearchParams } from "next/navigation";

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

  const router = useRouter();

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  }, [pathname, searchParams]);

  return (
    <MuiPagination
      color="primary"
      count={info.totalItems}
      page={info.page}
      renderItem={(item) => {
        if (!!item.page) {
          queryParams.set("page", item.page.toString());
        }
        const url = `${pathname}?${queryParams.toString()}`;

        return (
          <PaginationItem
            component={Link}
            href={url}
            scroll={false}
            {...item}
          />
        );
      }}
    />
  );
};

export default Pagination;
