import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import AddNewStudentModal from "./AddNewStudentModal";
import { Grid, List, ListItem, Stack, Button } from "@mui/material";
import moment from "moment";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCheck,
  faTimes,
  faTrash,
  faWindowMaximize,
} from "@fortawesome/free-solid-svg-icons";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import StudentInSchoolResponse from "../../../../ApiClient/Schools/Definitions/StudentInSchoolResponse";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import Tippy from "@tippyjs/react";
import { Link } from "react-router-dom";
import Swal from "sweetalert2";
import Notifications from "../../../../Notifications/Notifications";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
});
const mapDispatchToProps = (dispatch: any) => ({});
interface StudentsListProps {
  currentSchoolGuid?: string;
}
const StudentsList = (props: StudentsListProps): ReactElement => {
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
          .then((response) => {
            Notifications.showSuccessNotification(
              "personRemovedNotificationTitle",
              "personRemovedNotificationText"
            );
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
            <AddNewStudentModal
              show={showAddStudentModal}
              onHide={() => {
                setShowAddStudentModal(false);
              }}
            />
            <Button
              onClick={() => setShowAddStudentModal(true)}
              variant="outlined"
            >
              {t("addStudent")}
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
              mapper={(element: StudentInSchoolResponse, index) => (
                <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                  <Grid container spacing={2}>
                    <Grid item xs className="my-auto">
                      <div>{element.name}</div>
                    </Grid>
                    <Grid item xs className="my-auto">
                      <div>{element.surname}</div>
                    </Grid>
                    <Grid item xs className="my-auto">
                      <div>{moment(element.birthday).format("YYYY-MM-DD")}</div>
                    </Grid>
                    <Grid item xs className="my-auto">
                      <div>
                        <FontAwesomeIcon
                          icon={element.isActive ? faCheck : faTimes}
                        />
                      </div>
                    </Grid>
                    <Grid item xs={1} className="my-auto">
                      <div className="d-flex gap-1 flex-wrap">
                        <Link to={`/person/show/${element.guid}`}>
                          <Tippy
                            content={t("showPerson")}
                            arrow={true}
                            animation={"scale"}
                          >
                            <Button variant="outlined">
                              <FontAwesomeIcon icon={faWindowMaximize} />
                            </Button>
                          </Tippy>
                        </Link>
                        <Tippy
                          content={t("removePerson")}
                          arrow={true}
                          animation={"scale"}
                        >
                          <Button
                            variant="outlined"
                            color="error"
                            onClick={() =>
                              removePersonClickHandler(element.guid)
                            }
                          >
                            <FontAwesomeIcon icon={faTrash} />
                          </Button>
                        </Tippy>
                      </div>
                    </Grid>
                  </Grid>
                </ListItem>
              )}
              fetch={async (page: number) => {
                if (!props.currentSchoolGuid) return [];
                let resp = await SchoolsProxy.getStudentsInSchool(
                  props.currentSchoolGuid!,
                  page
                );
                return resp.data as [];
              }}
              effect={[
                props.currentSchoolGuid,
                showAddStudentModal,
                refreshEffectKey,
              ]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(StudentsList);
