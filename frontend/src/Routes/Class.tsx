import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import ClassIndex from "../Components/Class/ClassIndex";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface Props {}

class Class extends React.Component<Props> {
  render() {
    return (
      <Routes>
        <Route path="/show/:classGuid" element={<ClassIndex />}></Route>
      </Routes>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Class);
