import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import School from "../../../Shared/School";
import GetAccessibleSchoolsResponse from "../../../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
const mapStateToProps = (state: any) => {
  let currentSchool = state.common.schoolsList?.find(
    (school: GetAccessibleSchoolsResponse) =>
      school.guid == state.common.school.schoolGuid
  );
  return {
    activeSchoolGuid: currentSchool?.guid,
    activeSchoolName: currentSchool?.name,
    activeSchoolCity: currentSchool?.city,
    activeSchoolAddressLine: currentSchool?.addressLine1,
  };
};
const mapDispatchToProps = (dispatch: any) => ({});
interface ManageStudentsMenuProps {
  activeSchoolGuid?: string;
  activeSchoolName?: string;
  activeSchoolCity?: string;
  activeSchoolAddressLine?: string;
}
const ManageStudentsMenu = (props: ManageStudentsMenuProps): ReactElement => {
  const [activeTab, setActiveTab] = useState("studentsList");
  const { t } = useTranslation("manageStudentsMenu");
  return (
    <div className="d-flex flex-wrap gap-3 justify-content-center">
      <School
        name={props.activeSchoolName ?? ""}
        city={props.activeSchoolCity ?? ""}
        addresLine={props.activeSchoolAddressLine ?? ""}
      />
      <Link
        to="studentsList"
        className={
          "btn btn-outline-primary w-100 " +
          (activeTab == "studentsList" ? "active" : "")
        }
        onClick={() => {
          setActiveTab("studentsList");
        }}
      >
        {t("studentsList")}
      </Link>
      <Link
        to="invitations"
        className={
          "btn btn-outline-primary w-100 " +
          (activeTab == "invitations" ? "active" : "")
        }
        onClick={() => {
          setActiveTab("invitations");
        }}
      >
        {t("invitations")}
      </Link>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(ManageStudentsMenu);
