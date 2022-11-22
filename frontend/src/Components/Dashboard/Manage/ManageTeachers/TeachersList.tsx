import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import Swal from "sweetalert2";
import { Button, Grid, List, ListItem, Stack } from "@mui/material";
import AddNewTeacherModal from "./AddNewTeacherModal";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Tippy from "@tippyjs/react";
import { faCheck, faTimes, faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
import Notifications from "../../../../Notifications/Notifications";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import TeacherInSchoolResponse from "../../../../ApiClient/Schools/Definitions/Responses/TeacherInSchoolResponse";
import moment from "moment";
import { Link } from "react-router-dom";
import getCurrentSchoolReduxProxy from "../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux";
const mapStateToProps = (state: any) => ({
  currentSchool: getCurrentSchoolReduxProxy(state),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface ManageTeachersProps {
  currentSchool: any;
}
const TeachersList = (props: ManageTeachersProps): ReactElement => {
  const { t } = useTranslation("manageTeachers");
  const [showAddTeacherModal, setShowAddTeacherModal] = useState(false);
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
          .then((response) => {
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
          <h5 className="my-auto">{t("teachersList")}</h5>
          <div>
            <AddNewTeacherModal
              show={showAddTeacherModal}
              onHide={() => {
                setShowAddTeacherModal(false);
              }}
            />
            <Button onClick={() => setShowAddTeacherModal(true)} variant="outlined">
              {t("addTeacher")}
            </Button>
          </div>
        </div>
        <Stack className={"border rounded-3 my-1 p-3 bg-light"}>
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
        </Stack>
        <Stack>
          <List>
            <InfiniteScrollWrapper
              mapper={(element: TeacherInSchoolResponse, index) => (
                <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
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
                      <div className="d-flex gap-1 flex-wrap">
                        <Link to={`/person/show/${element.guid}`}>
                          <Tippy content={t("showPerson")} arrow={true} animation={"scale"}>
                            <Button variant="outlined">
                              <FontAwesomeIcon icon={faWindowMaximize} />
                            </Button>
                          </Tippy>
                        </Link>
                        <Tippy content={t("removePerson")} arrow={true} animation={"scale"}>
                          <Button variant="outlined" color="error" onClick={() => removePersonClickHandler(element.guid)}>
                            <FontAwesomeIcon icon={faTrash} />
                          </Button>
                        </Tippy>
                      </div>
                    </Grid>
                  </Grid>
                </ListItem>
              )}
              fetch={async (page: number) => {
                if (!props.currentSchool.schoolGuid) return [];
                let resp = await SchoolsProxy.getTeachersInSchool(props.currentSchool.schoolGuid!, page);
                return resp.data as [];
              }}
              effect={[props.currentSchool.schoolGuid, showAddTeacherModal, refreshEffectKey]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(TeachersList);
