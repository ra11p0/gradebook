import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import DashboardIndex from "../Components/Dashboard/DashboardIndex";
import DashboardNavigation from "../Components/Dashboard/DashboardNavigation";
import Absence from "../Components/Dashboard/Absence";
import Grades from "../Components/Dashboard/Grades";
import Subject from "../Components/Dashboard/Subject";
import Timetable from "../Components/Dashboard/Timetable";
import TeachersList from "../Components/Dashboard/Manage/ManageTeachers/TeachersList";
import Invitations from "../Components/Dashboard/Manage/ManageInvitations/Invitations";
import StudentsList from "../Components/Dashboard/Manage/ManageStudents/StudentsList";
import SchoolsList from "../Components/Dashboard/Manage/ManageSchool/SchoolsList";
import SchoolSelectedOnly from "../Components/Shared/SchoolSelectedOnly";
import ManageClasses from "../Components/Dashboard/Manage/ManageClasses/ManageClasses";
import { isLoggedInProxy } from "../Redux/ReduxProxy/getIsLoggedInReduxProxy";
import SettingsIndex from "../Components/Dashboard/Manage/Settings/SettingsIndex";

const mapStateToProps = (state: any) => ({
  isLoggedIn: isLoggedInProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardProps {
  isLoggedIn?: boolean;
}

class Dashboard extends React.Component<DashboardProps> {
  render() {
    return (
      <div className="">
        <div className="p-3 bg-light">
          <DashboardNavigation />
        </div>
        <div className="m-4">
          <Routes>
            <Route path="*" element={<DashboardIndex />}></Route>
            <Route path="absence" element={<Absence />}></Route>
            <Route path="grades" element={<Grades />}></Route>
            <Route path="subject" element={<Subject />}></Route>
            <Route path="timetable" element={<Timetable />}></Route>
            <Route
              path="manageStudents/*"
              element={
                <SchoolSelectedOnly>
                  <StudentsList />
                </SchoolSelectedOnly>
              }
            ></Route>
            <Route
              path="manageTeachers/*"
              element={
                <SchoolSelectedOnly>
                  <TeachersList />
                </SchoolSelectedOnly>
              }
            ></Route>
            <Route
              path="manageInvitations/*"
              element={
                <SchoolSelectedOnly>
                  <Invitations />
                </SchoolSelectedOnly>
              }
            ></Route>
            <Route
              path="manageClasses/*"
              element={
                <SchoolSelectedOnly>
                  <ManageClasses />
                </SchoolSelectedOnly>
              }
            ></Route>
            <Route path="manageSchool/*" element={<SchoolsList />}></Route>
            <Route path="settings/*" element={<SettingsIndex />}></Route>
          </Routes>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);
