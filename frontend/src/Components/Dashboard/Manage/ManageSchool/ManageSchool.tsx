import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Route, Routes } from "react-router-dom";
import SchoolsList from "./SchoolsList";
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface ManageSchoolProps {}
const ManageSchool = (props: ManageSchoolProps): ReactElement => {
  const { t } = useTranslation();
  return (
    <div className="row">
      <Routes>
        <Route path="/" element={<SchoolsList />} />
        <Route path="/SchoolsList" element={<SchoolsList />} />
      </Routes>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(ManageSchool);
