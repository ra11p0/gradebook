import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import ClassIndex from '../Components/Class/ClassIndex';

class Class extends React.Component {
  render(): ReactElement {
    return (
      <Routes>
        <Route path="/show/:classGuid" element={<ClassIndex />}></Route>
      </Routes>
    );
  }
}

export default Class;
