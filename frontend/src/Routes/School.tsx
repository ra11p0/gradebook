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
      <div className="m-3 card">
        <div className="card-body">
          <Routes>
            <Route path="/show/:schoolGuid" element={<SchoolIndex />}></Route>
          </Routes>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(School);
