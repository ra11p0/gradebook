import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import StudentIndex from "../Components/Student/StudentIndex";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardProps {}

class Student extends React.Component<DashboardProps> {
  render() {
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

export default connect(mapStateToProps, mapDispatchToProps)(Student);
