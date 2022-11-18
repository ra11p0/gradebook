import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Modal } from "react-bootstrap";
import { Button, FormControl, InputLabel, MenuItem, Select, Stack } from "@mui/material";
import StudentResponse from "../../../../ApiClient/Students/Definitions/Responses/StudentResponse";
import Person from "../../../Shared/Person";
import PersonSmall from "../../../Shared/PersonSmall";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import SchoolRolesEnum from "../../../../Common/Enums/SchoolRolesEnum";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import getCurrentSchoolReduxProxy from "../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux";
const mapStateToProps = (state: any) => ({
  currentSchool: getCurrentSchoolReduxProxy(state),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface AddInvitationModalProps {
  show: boolean;
  onHide: () => void;
  currentSchool: any;
}
const AddInvitationModal = (props: AddInvitationModalProps): ReactElement => {
  const { t } = useTranslation("addInvitationModal");
  const [inactiveStudents, setInactiveStudents] = useState([] as StudentResponse[]);
  const [selectedStudents, setSelectedStudents] = useState<string[]>([]);

  useEffect(() => {
    SchoolsProxy.getInactiveAccessibleStudentsInSchool(props.currentSchool.schoolGuid!).then((response) => {
      setInactiveStudents(response.data);
    });
  }, []);
  const sendButtonHandler = () => {
    SchoolsProxy.inviteMultiplePeople(
      {
        invitedPersonGuidArray: selectedStudents,
        role: SchoolRolesEnum.Student,
      },
      props.currentSchool.schoolGuid!
    ).then((response) => {
      props.onHide();
    });
  };
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addInvitation")}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Stack>
          <Stack>
            <div className="d-flex flex-wrap gap-1 justify-content-center mt-2">
              <>
                {inactiveStudents
                  .filter((student) => selectedStudents.includes(student.guid))
                  .map((student) => (
                    <div className="border rounded-3 p-0" key={student.guid}>
                      <PersonSmall name={student.name} surname={student.surname} />
                      <div className="d-inline mx-2">
                        <FontAwesomeIcon
                          icon={faTimes}
                          className="my-auto text-danger cursor-pointer"
                          onClick={() => setSelectedStudents(selectedStudents.filter((guid) => guid != student.guid))}
                        />
                      </div>
                    </div>
                  ))}
              </>
            </div>
          </Stack>
          <Stack>
            <FormControl>
              <InputLabel>{t("selectPeopleToInvite")}</InputLabel>
              <Select
                className="selectPeopleToInvite"
                multiple
                value={selectedStudents}
                onChange={(event) => {
                  const {
                    target: { value },
                  } = event;
                  setSelectedStudents(typeof value === "string" ? value.split(",") : value);
                }}
                renderValue={(selected) => selected.length}
                label={t("selectPeopleToInvite")}
              >
                {inactiveStudents.map((student) => (
                  <MenuItem key={student.guid} value={student.guid} className="row">
                    <Person guid={student.guid} name={student.name} surname={student.surname} birthday={student.birthday} noLink={true} />
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Stack>
        </Stack>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="outlined" onClick={sendButtonHandler}>
          {t("addInvitation")}
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(AddInvitationModal);
