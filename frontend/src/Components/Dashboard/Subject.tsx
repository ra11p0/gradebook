import React from "react";
import { connect } from "react-redux";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface SubjectProps {}

class Subject extends React.Component<SubjectProps> {
  render(): React.ReactNode {
    return <div>d-board Subject</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Subject);
