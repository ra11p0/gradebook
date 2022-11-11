import React, { useState } from "react";
import { Col, ListGroup, ListGroupItem, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import SubjectResponse from "../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import PermissionLevelEnum from "../../Common/Enums/Permissions/PermissionLevelEnum";
import InfiniteScrollWrapper from "../Shared/InfiniteScrollWrapper";
import PermissionsBlocker from "../Shared/PermissionsBlocker";
import NewSubjectModalWithButton from "./Manage/ManageSubjects/NewSubjectModalWithButton";

type Props = {};

function Subject({}: Props) {
  const [refreshKey, setRefreshKey] = useState(0);
  const navigate = useNavigate();
  return (
    <div>
      <Row>
        <Col>
          <div className="d-flex justify-content-end gap-2">
            <PermissionsBlocker
              allowingPermissions={[PermissionLevelEnum.Subjects_CanManageAssigned, PermissionLevelEnum.Subjects_CanManageAll]}
            >
              <NewSubjectModalWithButton
                onHide={() => {
                  setRefreshKey((e) => e + 1);
                }}
              />
            </PermissionsBlocker>
          </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <ListGroup>
            <InfiniteScrollWrapper
              mapper={(item: SubjectResponse, index: number) => (
                <ListGroupItem
                  className="cursor-pointer"
                  key={index}
                  onClick={() => {
                    navigate(`/subject/show/${item.guid}`);
                  }}
                >
                  {item.name}
                </ListGroupItem>
              )}
              fetch={async (page: number) => {
                return (await SchoolsProxy.subjects.getSubjectsInSchool(page)).data;
              }}
              effect={[refreshKey]}
            />
          </ListGroup>
        </Col>
      </Row>
    </div>
  );
}

export default Subject;
