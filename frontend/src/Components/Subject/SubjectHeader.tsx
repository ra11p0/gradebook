import React from "react";
import { Col, Row } from "react-bootstrap";
import PermissionLevelEnum from "../../Common/Enums/Permissions/PermissionLevelEnum";
import PermissionsBlocker from "../Shared/PermissionsBlocker";
import AddTeacherToSubjectModal from "./AddTeacherToSubjectModal";

type Props = {
  subjectName: string;
  subjectGuid: string;
  setRefreshKey: () => void;
};

function SubjectHeader(props: Props) {
  return (
    <div>
      <Row>
        <Col>
          <Row>
            <Col className="fs-3">{props.subjectName}</Col>
          </Row>
        </Col>
        <Col>
          <div className="d-flex justify-content-end gap-2">
            <PermissionsBlocker
              allowingPermissions={[PermissionLevelEnum.Subjects_CanManageAll, PermissionLevelEnum.Subjects_CanManageAssigned]}
            >
              <AddTeacherToSubjectModal
                onHide={() => {
                  props.setRefreshKey();
                }}
                subjectGuid={props.subjectGuid}
              />
            </PermissionsBlocker>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default SubjectHeader;
