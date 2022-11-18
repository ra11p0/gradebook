import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import PersonIndex from "../Components/Person/PersonIndex";
import getCurrentPersonReduxProxy from "../Redux/ReduxQueries/account/getCurrentPersonRedux";

interface AccountProps {
  currentPersonGuid?: string;
}

class Account extends React.Component<AccountProps> {
  render() {
    return (
      <Routes>
        <Route path="profile/*" element={<PersonIndex personGuid={this.props.currentPersonGuid} />} />
      </Routes>
    );
  }
}

export default connect(
  (state: any) => ({
    currentPersonGuid: getCurrentPersonReduxProxy(state)?.guid,
  }),
  (dispatch: any) => ({})
)(Account);
