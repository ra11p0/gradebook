import React from "react";
import { Route, Routes } from "react-router";
import SubjectIndex from "../Components/Subject/SubjectIndex";

type Props = {};

function Subject({}: Props) {
  return (
    <>
      <Routes>
        <Route path="/show/:subjectGuid" element={<SubjectIndex />}></Route>
      </Routes>
    </>
  );
}

export default Subject;
