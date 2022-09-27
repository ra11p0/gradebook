import { Button } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import ClassesProxy from "../../ApiClient/Classes/ClassesProxy";
import ClassResponse from "../../ApiClient/Schools/Definitions/Responses/ClassResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import PeoplePicker from "../Shared/PeoplePicker";

type Props = {};

function ClassIndex(props: Props) {
  const { t } = useTranslation("classIndex");
  const { classGuid } = useParams();
  const [_class, setClass] = useState<ClassResponse | null>(null);
  const [showStudentsPicker, setShowStudentsPicker] = useState(false);
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
          <Col>
            <div className="d-flex justify-content-end">
              <Button
                variant="outlined"
                onClick={() => {
                  setShowStudentsPicker((e) => !e);
                }}
              >
                {t("addStudentsToClass")}
              </Button>
              <PeoplePicker
                show={showStudentsPicker}
                onHide={() => {
                  setShowStudentsPicker(false);
                }}
                onConfirm={(studentsGuids: string[]) => {
                  console.dir(studentsGuids);
                }}
                getPeople={async (schoolGuid, discriminator: string, query: string, page: number) => {
                  return (await SchoolsProxy.searchPeople(schoolGuid, discriminator, query, page)).data;
                }}
              />
            </div>
          </Col>
        </Row>
      </div>
      <div className="m-4"></div>
    </div>
  );
}

export default ClassIndex;
