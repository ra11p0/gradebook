import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import SchoolIndex from '../Components/School/SchoolIndex';

class School extends React.Component {
  render(): ReactElement {
    return (
      <Routes>
        <Route path="/show/:schoolGuid" element={<SchoolIndex />}></Route>
      </Routes>
    );
  }
}

export default School;
