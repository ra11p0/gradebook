import React from "react";
import { Route, Routes } from "react-router-dom";
import { connect } from "react-redux";
import ManageStudentsMenu from "./ManageStudentsMenu";
import Invitations from "./Invitations";
import StudentsList from "./StudentsList";
import { t } from "i18next";

const mapStateToProps = (state: any) => ({
  isSchoolSelected: (() => {
    if (!state.common.school) return false;
    if (!state.common.school.schoolGuid) return false;
    return true;
  })(),
});

const mapDispatchToProps = (dispatch: any) => ({});

interface ManageStudentsProps {
  isSchoolSelected?: boolean;
}

class ManageStudents extends React.Component<ManageStudentsProps> {
  render(): React.ReactNode {
    return (
      <>
        {this.props.isSchoolSelected ? (
          <div className="row">
            <div className="col-3">
              <ManageStudentsMenu />
            </div>
            <div className="col">
              <Routes>
                <Route path="/" element={<StudentsList />} />
                <Route path="studentsList" element={<StudentsList />} />
                <Route path="invitations" element={<Invitations />} />
              </Routes>
            </div>
          </div>
        ) : (
          <>
            <div className="text-center">
              <>
                <div className="display-6 ">
                  <>{t("schoolIsNotSelected")} </>
                </div>
                <div>
                  <>{t("selectSchoolToManageStudents")}</>{" "}
                </div>
              </>
            </div>
          </>
        )}
      </>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ManageStudents);
