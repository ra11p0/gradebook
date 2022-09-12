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
      <div className="m-3 card">
        <div className="card-body">
          <Routes>
            <Route path="/show/:classGuid" element={<ClassIndex />}></Route>
          </Routes>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Class);
