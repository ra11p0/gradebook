import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { useParams } from "react-router-dom";
import ClassesProxy from "../../ApiClient/Classes/ClassesProxy";
import ClassResponse from "../../ApiClient/Schools/Definitions/ClassResponse";

type Props = {};

function ClassIndex(props: Props) {
  let { classGuid } = useParams();
  const [_class, setClass] = useState<ClassResponse | null>(null);
  useEffect(() => {
    ClassesProxy.getClass(classGuid!).then((classResponse) => {
      setClass(classResponse.data);
    });
  }, []);
  return (
    <div>
      <div className="bg-light p-3">
        <Row>
          <Col xs={"6"}>
            <Row>
              <Col className="my-auto">
                <h2>{_class?.name}</h2>
              </Col>
              <Col>{_class?.description}</Col>
            </Row>
          </Col>
        </Row>
      </div>
      <div className="m-4"></div>
    </div>
  );
}

export default ClassIndex;
