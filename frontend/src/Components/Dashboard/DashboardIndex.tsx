import React from "react";
import { connect } from "react-redux";

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
