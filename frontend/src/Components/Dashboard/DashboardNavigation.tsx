import React from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { Nav } from "react-bootstrap";
import { withTranslation } from "react-i18next";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface DashboardNavigationProps {
  t: any;
}
interface DashboardNavigationState {
  activeTab: string;
}

class DashboardNavigation extends React.Component<DashboardNavigationProps, DashboardNavigationState> {
  constructor(props: DashboardNavigationProps) {
    super(props);
    this.state = {
      activeTab: "",
    };
  }
  setActiveTab(tab: string) {
    this.setState({
      ...this.state,
      activeTab: tab,
    });
  }
  render(): React.ReactNode {
    const { t } = this.props;
    return (
      <div>
        <Nav className="d-flex gap-2 justify-content-end">
          <Link
            to="grades"
            className={"btn btn-outline-primary " + (this.state.activeTab == "grades" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("grades");
            }}
          >
            {t("grades")}
          </Link>
          <Link
            to="absence"
            className={"btn btn-outline-primary " + (this.state.activeTab == "absence" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("absence");
            }}
          >
            {t("absence")}
          </Link>
          <Link
            to="manageSubjects"
            className={"btn btn-outline-primary " + (this.state.activeTab == "subject" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("subject");
            }}
          >
            {t("subjects")}
          </Link>
          <Link to="timetable" className="btn btn-outline-primary">
            {" "}
            {t("timetable")}
          </Link>
          <Link
            to="manageStudents"
            className={"btn btn-outline-primary " + (this.state.activeTab == "manageStudents" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("manageStudents");
            }}
          >
            {t("manageStudents")}
          </Link>
          <Link
            to="manageTeachers"
            className={"btn btn-outline-primary " + (this.state.activeTab == "manageTeachers" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("manageTeachers");
            }}
          >
            {t("manageTeachers")}
          </Link>
          <Link
            to="manageClasses"
            className={"btn btn-outline-primary " + (this.state.activeTab == "manageClasses" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("manageClasses");
            }}
          >
            {t("manageClasses")}
          </Link>
          <Link
            to="manageInvitations"
            className={"btn btn-outline-primary " + (this.state.activeTab == "manageInvitations" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("manageInvitations");
            }}
          >
            {t("manageInvitations")}
          </Link>
          <Link
            to="manageSchool"
            className={"btn btn-outline-primary " + (this.state.activeTab == "manageSchool" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("manageSchool");
            }}
          >
            {t("manageSchool")}
          </Link>
          <Link
            to="settings"
            className={"btn btn-outline-primary " + (this.state.activeTab == "settings" ? "active" : "")}
            onClick={() => {
              this.setActiveTab("settings");
            }}
          >
            {t("settings")}
          </Link>
        </Nav>
      </div>
    );
  }
}

export default withTranslation("dashboardNavigation")(connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation));
