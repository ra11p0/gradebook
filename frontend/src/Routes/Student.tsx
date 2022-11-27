import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import StudentIndex from '../Components/Student/StudentIndex';

class Student extends React.Component {
  render(): ReactElement {
    return (
      <div className="m-3 card">
        <div className="card-body">
          <Routes>
            <Route path="/show/:schoolGuid" element={<StudentIndex />}></Route>
          </Routes>
        </div>
      </div>
    );
  }
}

export default Student;
