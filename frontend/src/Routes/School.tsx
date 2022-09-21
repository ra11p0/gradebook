import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import SchoolIndex from "../Components/School/SchoolIndex";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface Props {}

class School extends React.Component<Props> {
  render() {
    return (
      <Routes>
        <Route path="/show/:schoolGuid" element={<SchoolIndex />}></Route>
      </Routes>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(School);
