import { faSchool } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Col, Row } from "react-bootstrap";

interface SchoolProps {
  name: string;
  city: string;
  addresLine: string;
  className?: string;
}
function School(props: SchoolProps) {
  return (
    <div
      className={`bg-light border rounded-3 m-1 p-2 ${props.className ?? ""}`}
    >
      <Row>
        <Col xs={2} className="my-auto text-center">
          <FontAwesomeIcon icon={faSchool} />
        </Col>
        <Col>
          <Row>{props.name}</Row>
          <Row>{props.city}</Row>
        </Col>
        <Col className="my-auto">{props.addresLine}</Col>
      </Row>
    </div>
  );
}

export default School;
