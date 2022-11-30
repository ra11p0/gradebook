import React from 'react';
import { Route, Routes } from 'react-router-dom';
import LoginForm from '../Components/Account/Login/LoginForm';
import RegisterForm from '../Components/Account/Register/RegisterForm';

class Index extends React.Component {
  render(): React.ReactNode {
    return (
      <div>
        <div className="App row m-md-2 gap-md-2 ">
          <div className="col-md"></div>
          <div className="col-md-5 p-md-3">
            <Routes>
              <Route path="*" element={<LoginForm />} />
              <Route path="register" element={<RegisterForm />} />
            </Routes>
          </div>
          <div className="col-md"></div>
        </div>
      </div>
    );
  }
}

export default Index;
