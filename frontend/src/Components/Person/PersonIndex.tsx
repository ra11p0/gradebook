import React from "react";
import { useParams } from "react-router-dom";

type Props = {};

function PersonIndex(props: Props) {
  let { personGuid } = useParams();
  return <div>{`PersonPage ${personGuid}`}</div>;
}

export default PersonIndex;
