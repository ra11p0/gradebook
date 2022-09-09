import React from "react";
import { Link, Navigate } from "react-router-dom";
import { connect } from "react-redux";
import { logIn } from "../../Actions/Account/accountActions";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardIndexProps {
  isLoggedIn: boolean;
}

class DashboardIndex extends React.Component<DashboardIndexProps> {
  render(): React.ReactNode {
    return <div>d-board index</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(DashboardIndex);
