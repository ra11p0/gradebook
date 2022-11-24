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
import getIsLoggedInReduxProxy from "../Redux/ReduxQueries/account/getIsLoggedInRedux";
import SettingsIndex from "../Components/Dashboard/Manage/Settings/SettingsIndex";
import EducationCycle from "../Components/Dashboard/Manage/EducationCycle/EducationCycle";
import getHasPermissionRedux from "../Redux/ReduxQueries/account/getHasPermissionRedux";
import PermissionLevelEnum from "../Common/Enums/Permissions/PermissionLevelEnum";

const mapStateToProps = (state: any) => ({
  isLoggedIn: getIsLoggedInReduxProxy(state),
  permissions: {
    hasPermissionToEducatonCycles: getHasPermissionRedux([PermissionLevelEnum.EducationCycles_CanCreateAndDelete, PermissionLevelEnum.EducationCycles_ViewOnly], state)
  }
});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardProps {
  isLoggedIn?: boolean;
  permissions: {
    hasPermissionToEducatonCycles: boolean;
  }
}

class Dashboard extends React.Component<DashboardProps> {
  render() {
    return (
      <div>
        <div className="p-3 bg-light">
          <DashboardNavigation />
        </div>
        <div className="m-4">
          <Routes>
            <Route path="*" element={<DashboardIndex />} />
            <Route path="absence" element={<Absence />} />
            <Route path="grades" element={<Grades />} />
            {
              this.props.permissions.hasPermissionToEducatonCycles && (
                <Route
                  path="educationCycle/*"
                  element={
                    <SchoolSelectedOnly>
                      <EducationCycle />
                    </SchoolSelectedOnly>
                  }
                />
              )
            }

            <Route
              path="manageSubjects"
              element={
                <SchoolSelectedOnly>
                  <Subject />
                </SchoolSelectedOnly>
              }
            ></Route>
            <Route path="timetable" element={<Timetable />} />
            <Route
              path="manageStudents/*"
              element={
                <SchoolSelectedOnly>
                  <StudentsList />
                </SchoolSelectedOnly>
              }
            />
            <Route
              path="manageTeachers/*"
              element={
                <SchoolSelectedOnly>
                  <TeachersList />
                </SchoolSelectedOnly>
              }
            />
            <Route
              path="manageInvitations/*"
              element={
                <SchoolSelectedOnly>
                  <Invitations />
                </SchoolSelectedOnly>
              }
            />
            <Route
              path="manageClasses/*"
              element={
                <SchoolSelectedOnly>
                  <ManageClasses />
                </SchoolSelectedOnly>
              }
            />
            <Route path="manageSchool/*" element={<SchoolsList />} />
            <Route path="settings/*" element={<SettingsIndex />} />
          </Routes>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);
