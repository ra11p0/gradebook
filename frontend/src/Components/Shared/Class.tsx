import { faChalkboardTeacher } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import moment from "moment";
import React from "react";
import { Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

type Props = {
  guid: string;
  name: string;
  description?: string;
  createdDate: Date;
  noLink?: boolean;
  className?: string;
};

function Class(props: Props) {
  const { t } = useTranslation("class");
  return (
    <>
      <Link to={props.noLink ? `#` : `/class/show/${props.guid}`} className={`text-reset`}>
        <div className={`bg-light border rounded-3 m-1 p-2 ${props.className}`}>
          <Row>
            <Col xs={2} className="my-auto text-center">
              <FontAwesomeIcon icon={faChalkboardTeacher} />
            </Col>
            <Col>
              <Row>{props.name}</Row>
              <Row>{moment.utc(props.createdDate).local().format("Y")}</Row>
            </Col>
            <Col className="my-auto">{props.description}</Col>
          </Row>
        </div>
      </Link>
    </>
  );
}

export default Class;
