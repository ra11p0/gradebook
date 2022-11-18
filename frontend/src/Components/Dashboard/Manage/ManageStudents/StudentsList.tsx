import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Grid, List, ListItem, Stack, Button } from "@mui/material";
import moment from "moment";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheck, faTimes, faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import StudentInSchoolResponse from "../../../../ApiClient/Schools/Definitions/Responses/StudentInSchoolResponse";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import Tippy from "@tippyjs/react";
import { Link } from "react-router-dom";
import Swal from "sweetalert2";
import Notifications from "../../../../Notifications/Notifications";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
import getCurrentSchoolReduxProxy from "../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux";
import AddNewStudentModalWithButton from "./AddNewStudentModalWithButton";
import PermissionsBlocker from "../../../Shared/PermissionsBlocker";
import PermissionLevelEnum from "../../../../Common/Enums/Permissions/PermissionLevelEnum";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: getCurrentSchoolReduxProxy(state)?.schoolGuid,
});
const mapDispatchToProps = (dispatch: any) => ({});
interface StudentsListProps {
  currentSchoolGuid: string | undefined;
}
function StudentsList(props: StudentsListProps) {
  const { t } = useTranslation("studentsList");
  const [showAddStudentModal, setShowAddStudentModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);

  const removePersonClickHandler = (personGuid: string) => {
    Swal.fire({
      title: t("removePerson"),
      text: t("youSureRemovePerson"),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t("yes"),
      denyButtonText: t("no"),
      icon: "warning",
    }).then((result) => {
      if (result.isConfirmed) {
        PeopleProxy.removePerson(personGuid)
          .then(() => {
            Notifications.showSuccessNotification("personRemovedNotificationTitle", "personRemovedNotificationText");
            setRefreshEffectKey((k) => k + 1);
          })
          .catch(Notifications.showApiError);
      }
    });
  };

  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("studentsList")}</div>
          <div>
            <PermissionsBlocker allowingPermissions={[PermissionLevelEnum.Students_CanCreateAndDelete]}>
              <AddNewStudentModalWithButton setShowAddStudentModal={setShowAddStudentModal} showAddStudentModal={showAddStudentModal} />
            </PermissionsBlocker>
          </div>
        </div>
        <Stack className={"border rounded-3 my-1 p-3 bg-light"}>
          <ListHeader />
        </Stack>
        <Stack>
          <List>
            <InfiniteScrollWrapper
              mapper={(element: StudentInSchoolResponse, index) => (
                <StudentRow {...element} removePersonClickHandler={removePersonClickHandler} key={index} />
              )}
              fetch={async (page: number) => {
                if (!props.currentSchoolGuid) return [];
                let resp = await SchoolsProxy.getStudentsInSchool(props.currentSchoolGuid!, page);
                return resp.data as [];
              }}
              effect={[props.currentSchoolGuid, showAddStudentModal, refreshEffectKey]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

function StudentRow(element: StudentInSchoolResponse & { removePersonClickHandler: (guid: string) => void }) {
  return (
    <ListItem className={"border rounded-3 my-1 p-3"}>
      <Grid container spacing={2}>
        <Grid item xs className="my-auto">
          <div>{element.name}</div>
        </Grid>
        <Grid item xs className="my-auto">
          <div>{element.surname}</div>
        </Grid>
        <Grid item xs className="my-auto">
          <div>{moment.utc(element.birthday).local().format("L")}</div>
        </Grid>
        <Grid item xs className="my-auto">
          <div>
            <FontAwesomeIcon icon={element.isActive ? faCheck : faTimes} />
          </div>
        </Grid>
        <Grid item xs={1} className="my-auto">
          <StudentActions {...element} />
        </Grid>
      </Grid>
    </ListItem>
  );
}

function StudentActions(element: StudentInSchoolResponse & { removePersonClickHandler: (guid: string) => void }) {
  const { t } = useTranslation("studentsList");
  return (
    <div className="d-flex gap-1 flex-wrap">
      <Link to={`/person/show/${element.guid}`}>
        <Tippy content={t("showPerson")} arrow={true} animation={"scale"}>
          <Button variant="outlined" className="showProfileButton">
            <FontAwesomeIcon icon={faWindowMaximize} />
          </Button>
        </Tippy>
      </Link>
      <PermissionsBlocker allowingPermissions={[PermissionLevelEnum.Students_CanCreateAndDelete]}>
        <Tippy content={t("removePerson")} arrow={true} animation={"scale"}>
          <Button variant="outlined" color="error" onClick={() => element.removePersonClickHandler(element.guid)}>
            <FontAwesomeIcon icon={faTrash} />
          </Button>
        </Tippy>
      </PermissionsBlocker>
    </div>
  );
}

function ListHeader() {
  const { t } = useTranslation("studentsList");
  return (
    <Grid container spacing={2}>
      <Grid item xs>
        <div>{t("name")}</div>
      </Grid>
      <Grid item xs>
        <div>{t("surname")}</div>
      </Grid>
      <Grid item xs>
        <div>{t("birthday")}</div>
      </Grid>
      <Grid item xs>
        <div>{t("isActive")}</div>
      </Grid>
      <Grid item xs={1}>
        <div>{t("actions")}</div>
      </Grid>
    </Grid>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(StudentsList);
