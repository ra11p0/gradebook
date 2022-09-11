import React from "react";
import { useParams } from "react-router-dom";

type Props = {};

function SchoolIndex(props: Props) {
  let { schoolGuid } = useParams();
  return <div>{`SchoolPage ${schoolGuid}`}</div>;
}

export default SchoolIndex;
