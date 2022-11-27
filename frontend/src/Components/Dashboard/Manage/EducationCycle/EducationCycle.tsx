import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router';
import CyclesList from './CyclesList/CyclesList';
import NewCycleForm from './NewCycleForm/NewCycleForm';

function EducationCycle(): ReactElement {
  return (
    <>
      <Routes>
        <Route path="" element={<CyclesList />} />
        <Route path="new" element={<NewCycleForm />} />
      </Routes>
    </>
  );
}

export default EducationCycle;
