import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import PersonIndex from '../Components/Person/PersonIndex';

class Person extends React.Component {
  render(): ReactElement {
    return (
      <Routes>
        <Route path="/show/:personGuid/*" element={<PersonIndex />}></Route>
      </Routes>
    );
  }
}

export default Person;
