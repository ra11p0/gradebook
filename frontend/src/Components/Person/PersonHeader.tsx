import React, { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { ClassResponse } from "../../ApiClient/People/Definitions/PersonResponse";
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";

type Props = {
  personName: string;
  personSurname: string;
  personSchoolRole: SchoolRolesEnum;
  activeClass: ClassResponse | undefined;
};

function PersonHeader(props: Props) {
  const [activeTab, setActiveTab] = useState<string>("");
  const { t } = useTranslation("personNavigation");
  return (
    <div>
      <Row>
        <Col>
          <Row>
            <Col className="fs-3">{`${props.personName} ${props.personSurname}`}</Col>
            {props.personSchoolRole === SchoolRolesEnum.Student && props.activeClass && (
              <Col>
                <Link className="btn btn-link text-reset" to={`/class/show/${props.activeClass.guid}`}>
                  {`${t("class")}: ${props.activeClass.name}`}
                </Link>
              </Col>
            )}
          </Row>
        </Col>
        <Col>
          <div className="d-flex justify-content-end gap-2">
            <Link
              to=""
              className={"btn btn-outline-primary " + (activeTab == "overview" ? "active" : "")}
              onClick={() => {
                setActiveTab("overview");
              }}
            >
              {t("overview")}
            </Link>
            <Link
              to="permissions"
              className={" permissions btn btn-outline-primary " + (activeTab == "permissions" ? "active" : "")}
              onClick={() => {
                setActiveTab("permissions");
              }}
            >
              {t("permissions")}
            </Link>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default PersonHeader;
