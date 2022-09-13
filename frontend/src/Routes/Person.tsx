import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import PersonIndex from "../Components/Person/PersonIndex";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardProps {}

class Person extends React.Component<DashboardProps> {
  render() {
    return (
      <Routes>
        <Route path="/show/:personGuid" element={<PersonIndex />}></Route>
      </Routes>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Person);
