import React, { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

type Props = {
  personName: string;
  personSurname: string;
};

function PersonNavigation(props: Props) {
  const [activeTab, setActiveTab] = useState<string>("");
  const { t } = useTranslation("personNavigation");
  return (
    <div>
      <Row>
        <Col>
          <Row>
            <Col className="fs-3">{`${props.personName} ${props.personSurname}`}</Col>
            <Col> class placeholder </Col>
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

export default PersonNavigation;
