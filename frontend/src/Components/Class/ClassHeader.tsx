import React from "react";
import { Col, ListGroup, ListGroupItem, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import StudentInClassResponse from "../../ApiClient/Classes/Definitions/Responses/StudentInClassResponse";
import TeachersInClassResponse from "../../ApiClient/Classes/Definitions/Responses/TeachersInClassResponse";
import ClassResponse from "../../ApiClient/Schools/Definitions/Responses/ClassResponse";
import ManageClassOwners from "./ManageClassOwners";
import ManageStudentsInClass from "./ManageStudentsInClass";

type Props = {
  class: ClassResponse;
  classGuid: string;
  classOwners: TeachersInClassResponse[];
  studentsInClass: StudentInClassResponse[];
  setRefreshKey: (func: (key: any) => void) => void;
  setStudentsInClass: (students: StudentInClassResponse[]) => void;
  setClassOwners: (owners: TeachersInClassResponse[]) => void;
};

function ClassHeader(props: Props) {
  const { t } = useTranslation("classIndex");
  return (
    <Row>
      <Col xs={"6"}>
        <Row>
          <Col className="my-auto">
            <h2>{props.class?.name}</h2>
          </Col>
          <Col>
            <Row>
              <Col>{props.class?.description}</Col>
            </Row>
            <Row>
              <Col>
                {t("owners")}:
                {props.classOwners.map((e, i) => (
                  <div key={i}>
                    <Link className="text-reset btn btn-link" key={i} to={`/person/show/${e.guid}`}>{`${e.name} ${e.surname}`}</Link>
                  </div>
                ))}
              </Col>
            </Row>
          </Col>
        </Row>
      </Col>
      <Col>
        <div className="d-flex justify-content-end gap-2">
          <ManageStudentsInClass
            classGuid={props.classGuid}
            studentsInClass={props.studentsInClass}
            setStudentsInClass={props.setStudentsInClass}
            setRefreshKey={props.setRefreshKey}
          />

          <ManageClassOwners
            classGuid={props.classGuid}
            classOwners={props.classOwners}
            setClassOwners={props.setClassOwners}
            setRefreshKey={props.setRefreshKey}
          />
        </div>
      </Col>
    </Row>
  );
}

export default ClassHeader;
