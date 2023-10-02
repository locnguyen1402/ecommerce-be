"use client";

import { ReactNode, useMemo, useState } from "react";

import qs from "querystring";

import { useQuery } from "@tanstack/react-query";

import { HttpUtils } from "@/utils";

type Props<T> = {
  url: string;
  queryParams?: Record<string, any>;

  children: (val: { isLoading: boolean; data: T[] }) => ReactNode;
};

const QueryExecutor = <T extends any>(props: Props<T>) => {
  const queryString = useMemo(() => {
    if (!props.queryParams) {
      return "";
    }
    return qs.stringify(props.queryParams);
  }, [props.queryParams]);

  const context = useQuery({
    queryKey: [props.url, queryString],
    queryFn: () => {
      return HttpUtils.get<T[]>(`${props.url}?${queryString}`);
    },
  });

  const data = context.data?.data || [];

  return props.children({
    isLoading: context.isLoading,
    data,
  });
};

export default QueryExecutor;
