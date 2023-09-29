import { FC, Fragment, ReactNode } from "react";

import { Skeleton, Stack } from "@mui/material";

type Props = {
  pageSize: number;

  skeletonCard: FC;
};

const SkeletonCardList = (props: Props) => {
  const Card = props.skeletonCard;
  return (
    <>
      {Array.from(Array(props.pageSize).keys()).map((_, idx) => {
        return <Fragment key={idx}>{<Card />}</Fragment>;
      })}
    </>
  );
};

export default SkeletonCardList;
