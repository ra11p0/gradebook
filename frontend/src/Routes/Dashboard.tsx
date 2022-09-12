import { connect } from "react-redux";
import React from "react";
import { Route, Routes } from "react-router-dom";
import DashboardIndex from "../Components/Dashboard/DashboardIndex";
import DashboardNavigation from "../Components/Dashboard/DashboardNavigation";
import Absence from "../Components/Dashboard/Absence";
import Grades from "../Components/Dashboard/Grades";
import Subject from "../Components/Dashboard/Subject";
import Timetable from "../Components/Dashboard/Timetable";
import ManageTeachers from "../Components/Dashboard/Manage/ManageTeachers/ManageTeachers";
import Invitations from "../Components/Dashboard/Manage/ManageInvitations/Invitations";
import StudentsList from "../Components/Dashboard/Manage/ManageStudents/StudentsList";
import SchoolsList from "../Components/Dashboard/Manage/ManageSchool/SchoolsList";
import SchoolSelectedOnly from "../Components/Shared/SchoolSelectedOnly";
import ManageClasses from "../Components/Dashboard/Manage/ManageClasses/ManageClasses";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
  isSuperAdmin: state.common.session.roles.includes("SuperAdmin"),
});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardProps {
  isLoggedIn?: boolean;
  isSuperAdmin: boolean;
}

class Dashboard extends React.Component<DashboardProps> {
  render() {
    return (
      <div className="m-3 card">
        <div className="card-header">
          <DashboardNavigation />
        </div>
        <div className="card-body">
          <Routes>
            <Route path="*" element={<DashboardIndex />}></Route>
            <Route path="absence" element={<Absence />}></Route>
            <Route path="grades" element={<Grades />}></Route>
            <Route path="subject" element={<Subject />}></Route>
            <Route path="timetable" element={<Timetable />}></Route>
            {this.props.isSuperAdmin && (
              <>
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
                      <ManageTeachers />
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
              </>
            )}
          </Routes>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);
