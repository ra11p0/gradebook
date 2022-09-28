import React from "react";
import { Route, Routes } from "react-router-dom";
import LoginForm from "../Components/Account/Login/LoginForm";
import RegisterForm from "../Components/Account/Register/RegisterForm";

class Index extends React.Component {
  render(): React.ReactNode {
    return (
      <div>
        <div className="App row m-2 gap-2 border rounded-3">
          <div className="col"></div>
          <div className="col-6 p-3">
            <Routes>
              <Route path="*" element={<LoginForm />} />
              <Route path="register" element={<RegisterForm />} />
            </Routes>
          </div>
          <div className="col"></div>
        </div>
      </div>
    );
  }
}

export default Index;
