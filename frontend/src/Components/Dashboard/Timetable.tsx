import React from "react";
import { connect } from "react-redux";
import { isLoggedInProxy } from "../../ReduxProxy/isLoggedInProxy";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface TimetableProps {}

class Timetable extends React.Component<TimetableProps> {
  render(): React.ReactNode {
    return <div>d-board Timetable</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Timetable);
