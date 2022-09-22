import React from "react";
import { connect } from "react-redux";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
});

const mapDispatchToProps = (dispatch: any) => ({});

interface TimetableProps {
  isLoggedIn: boolean;
}

class Timetable extends React.Component<TimetableProps> {
  render(): React.ReactNode {
    return <div>d-board Timetable</div>;
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Timetable);
