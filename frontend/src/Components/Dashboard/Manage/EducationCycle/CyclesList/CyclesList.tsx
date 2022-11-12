import React from "react";
import { Col, Row } from "react-bootstrap";
import Header from "./Header";

type Props = {};

function CyclesList({}: Props) {
  return (
    <Row>
      <Col>
        <Row>
          <Col>
            <Header />
          </Col>
        </Row>
        <Row>
          <Col></Col>
        </Row>
      </Col>
    </Row>
  );
}

export default CyclesList;
