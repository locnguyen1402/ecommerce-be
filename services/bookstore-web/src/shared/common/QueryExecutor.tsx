"use client";

import { ReactNode } from "react";

import { useQuery } from "@tanstack/react-query";

type Props<T> = {
  children: (val: { isLoading: boolean; data: T[] }) => ReactNode;
  queryPromise: () => Promise<SuccessResponse<T[]>>;

  queryKey?: string[];
};

const QueryExecutor = <T extends any>(props: Props<T>) => {
  const context = useQuery({
    queryKey: !!props.queryKey?.length
      ? props.queryKey
      : [Math.random().toString()],
    queryFn: props.queryPromise,
  });

  const data = context.data?.data || [];
  return props.children({
    isLoading: context.isLoading,
    data,
  });
};

export default QueryExecutor;
