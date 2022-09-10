import React, { ReactElement, useState } from "react";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

function ManageSchoolMenu(): ReactElement {
  const { t } = useTranslation("manageSchoolMenu");
  const [activeTab, setActiveTab] = useState("studentsList");
  return (
    <div className="d-flex flex-wrap gap-3">
      <Link
        to="schoolsList"
        className={
          "btn btn-outline-primary w-100 " +
          (activeTab == "schoolsList" ? "active" : "")
        }
        onClick={() => {
          setActiveTab("schoolsList");
        }}
      >
        {t("schoolsList")}
      </Link>
    </div>
  );
}

export default ManageSchoolMenu;
