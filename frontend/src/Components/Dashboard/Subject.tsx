import React from "react";
import { connect } from "react-redux";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
});

const mapDispatchToProps = (dispatch: any) => ({});

interface SubjectProps {
  isLoggedIn: boolean;
}

class Subject extends React.Component<SubjectProps> {
  render(): React.ReactNode {
    return <div>d-board Subject</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Subject);
