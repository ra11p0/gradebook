import React from "react";
import { connect } from "react-redux";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface AbsenceProps {}

class Absence extends React.Component<AbsenceProps> {
  render(): React.ReactNode {
    return <div>d-board Absence</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Absence);
