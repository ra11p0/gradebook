import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router';
import SubjectIndex from '../Components/Subject/SubjectIndex';

function Subject(): ReactElement {
  return (
    <>
      <Routes>
        <Route path="/show/:subjectGuid" element={<SubjectIndex />}></Route>
      </Routes>
    </>
  );
}

export default Subject;
