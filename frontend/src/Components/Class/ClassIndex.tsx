import { Button } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import ClassesProxy from "../../ApiClient/Classes/ClassesProxy";
import ClassResponse from "../../ApiClient/Schools/Definitions/Responses/ClassResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../Notifications/Notifications";
import LoadingScreen from "../Shared/LoadingScreen";
import PeoplePicker from "../Shared/PeoplePicker";

type Props = {};

function ClassIndex(props: Props) {
  const { t } = useTranslation("classIndex");
  const { classGuid } = useParams();
  const [_class, setClass] = useState<ClassResponse | null>(null);
  const [showStudentsPicker, setShowStudentsPicker] = useState(false);
  const [showTeachersPicker, setShowTeachersPicker] = useState(false);
  const [studentsInClass, setStudentsInClass] = useState<string[]>([]);
  const [classOwners, setClassOwners] = useState<string[]>();
  const [isReady, setIsReady] = useState(false);
  useEffect(() => {
    Promise.all([
      ClassesProxy.getClass(classGuid!).then(r => setClass(r.data)).catch(Notifications.showApiError),
      ClassesProxy.getStudentsInClass(classGuid!).then(r => setStudentsInClass(r.data.map(e => e.guid))).catch(Notifications.showApiError),
      ClassesProxy.getTeachersInClass(classGuid!).then(r => setClassOwners(r.data.map(e => e.guid))).catch(Notifications.showApiError)])
      .then(() => setIsReady(true));
  }, [classGuid]);
  return (
    <LoadingScreen isReady={isReady}>
      <>
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
                    setStudentsInClass(studentsGuids);
                  }}
                  selectedPeople={studentsInClass}
                  getPeople={async (schoolGuid, discriminator: string, query: string, page: number) => {
                    return (await SchoolsProxy.searchPeople(schoolGuid, 'Student', query, page)).data;
                  }}
                />
                <Button
                  variant="outlined"
                  onClick={() => {
                    setShowTeachersPicker((e) => !e);
                  }}
                >
                  {t("addClassOwners")}
                </Button>
                <PeoplePicker
                  show={showTeachersPicker}
                  onHide={() => {
                    setShowTeachersPicker(false);
                    //ClassesProxy.a
                  }}
                  onConfirm={(teachers: string[]) => {
                    setClassOwners(teachers);
                  }}
                  selectedPeople={classOwners}
                  getPeople={async (schoolGuid, discriminator: string, query: string, page: number) => {
                    return (await SchoolsProxy.searchPeople(schoolGuid, 'Teacher', query, page)).data;
                  }}
                />
              </div>
            </Col>
          </Row>
        </div>
        <div className="m-4"></div>
      </>
    </LoadingScreen>
  );
}

export default ClassIndex;
