import React from "react";
import { useParams } from "react-router-dom";

type Props = {};

function StudentIndex(props: Props) {
  let { studentGuid } = useParams();
  return <div>{`StudentPage ${studentGuid}`}</div>;
}

export default StudentIndex;
