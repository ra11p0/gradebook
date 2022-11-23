import React from "react";
import { Col, Row } from "react-bootstrap";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import InfiniteScrollWrapper from "../../../../Shared/InfiniteScrollWrapper";
import Header from "./Header";

type Props = {};

function CyclesList({ }: Props) {
  return (
    <Row>
      <Col>
        <Row>
          <Col>
            <Header />
          </Col>
        </Row>
        <Row>
          <Col>
            <InfiniteScrollWrapper
              mapper={(item: any, index: number) => (<div>{item.name}</div>)}
              fetch={async (page: number) => {
                return (await SchoolsProxy.educationCycles.getEducationCyclesInSchool(page)).data
              }}
            />

          </Col>
        </Row>
      </Col>
    </Row>
  );
}

export default CyclesList;
