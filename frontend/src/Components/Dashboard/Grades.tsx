import React from "react";
import { connect } from "react-redux";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
});

const mapDispatchToProps = (dispatch: any) => ({});

interface GradesProps {
  isLoggedIn: boolean;
}

class Grades extends React.Component<GradesProps> {
  render(): React.ReactNode {
    return <div>d-board Grades</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Grades);
