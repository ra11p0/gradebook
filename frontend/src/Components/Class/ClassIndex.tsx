import React from "react";
import { useParams } from "react-router-dom";

type Props = {};

function ClassIndex(props: Props) {
  let { classGuid } = useParams();
  return <div>{`ClassPage ${classGuid}`}</div>;
}

export default ClassIndex;
