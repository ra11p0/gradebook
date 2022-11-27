import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import EducationCycleIndex from '../Components/EducationCycle/EducationCycle';

function EducationCycle(): ReactElement {
  return (
    <Routes>
      <Route
        path="/show/:educationCycleGuid"
        element={<EducationCycleIndex />}
      ></Route>
    </Routes>
  );
}

export default EducationCycle;
