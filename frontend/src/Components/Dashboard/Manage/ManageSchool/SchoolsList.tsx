import { Button, Grid, List, ListItem, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import Tippy from "@tippyjs/react";
import Swal from "sweetalert2";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../../../Notifications/Notifications";
import { Row } from "react-bootstrap";
import AccountProxy from "../../../../ApiClient/Accounts/AccountsProxy";
import getCurrentUserIdReduxProxy from "../../../../Redux/ReduxProxy/getCurrentUserIdReduxProxy";
import getSchoolsListReduxProxy from "../../../../Redux/ReduxProxy/getSchoolsListReduxProxy";
import GetSchoolResponse from "../../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse";
import setSchoolsListReduxWrapper, { setSchoolsListAction } from "../../../../Redux/ReduxWrappers/setSchoolsListReduxWrapper";
import setSchoolReduxWrapper, { setSchoolAction } from "../../../../Redux/ReduxWrappers/setSchoolReduxWrapper";
import JoinSchoolModal from "./JoinSchoolModal";
import getCurrentSchoolReduxProxy from "../../../../Redux/ReduxProxy/getCurrentSchoolReduxProxy";
import getCurrentPersonReduxProxy, { CurrentPersonProxyResult } from "../../../../Redux/ReduxProxy/getCurrentPersonReduxProxy";

interface SchoolsListProps {
  userId?: string;
  schoolsList: GetSchoolResponse[] | null;
  currentSchoolGuid?: string;
  setSchoolsList?: (action: setSchoolsListAction) => void;
  setCurrentSchool?: (action: setSchoolAction) => void;
  currentPerson?: CurrentPersonProxyResult;
}

function SchoolsList(props: SchoolsListProps) {
  const { t } = useTranslation("schoolsList");
  const [showJoinSchoolModal, setShowJoinSchoolModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);

  useEffect(() => {
    AccountProxy.getAccessibleSchools(props.userId!).then((schoolsResponse) => {
      if (!schoolsResponse.data.map((e) => e.school.guid).includes(props.currentSchoolGuid!)) {
        if (schoolsResponse.data.length != 0)
          props.setCurrentSchool!({
            schoolGuid: schoolsResponse.data[0].school.guid,
            schoolName: schoolsResponse.data[0].school.name,
          });
        else props.setCurrentSchool!({ schoolName: "", schoolGuid: "" });
      }
    });
  }, [showJoinSchoolModal, refreshEffectKey]);

  function removeSchoolClickHandler(schoolGuid: string) {
    Swal.fire({
      title: t("removeSchool"),
      text: t("youSureRemoveSchool"),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t("yes"),
      denyButtonText: t("no"),
      icon: "warning",
    }).then((result) => {
      if (result.isConfirmed) {
        SchoolsProxy.removeSchool(schoolGuid)
          .then((response) => {
            Notifications.showSuccessNotification("schoolRemovedNotificationTitle", "schoolRemovedNotificationText");
            setRefreshEffectKey((k) => k + 1);
          })
          .catch((err) => {
            Notifications.showApiError(err);
          });
      }
    });
  }

  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("managedSchools")}</div>
          <div className="d-flex gap-2">
            <JoinSchoolModal
              show={showJoinSchoolModal}
              onHide={() => {
                setShowJoinSchoolModal(false);
              }}
              person={props.currentPerson}
            />
            <Button className="addSchoolButton" onClick={() => setShowJoinSchoolModal(true)} variant={"outlined"}>
              {t("addSchool")}
            </Button>
          </div>
        </div>
        <Stack className={"border rounded-3 my-1 p-3 bg-light"}>
          <Grid container spacing={2}>
            <Grid item xs>
              <div>{t("name")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("address")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("postalCode")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("city")}</div>
            </Grid>
            <Grid item xs={1}>
              <div>{t("actions")}</div>
            </Grid>
          </Grid>
        </Stack>
        <Stack>
          <List>
            {props.schoolsList?.map((school, index) => (
              <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                <Grid container spacing={2}>
                  <Grid item xs className="my-auto">
                    <div>{school.name}</div>
                  </Grid>
                  <Grid item xs className="my-auto">
                    <Stack>
                      <div>{school.addressLine1}</div>
                      {school.addressLine2 && <div>{school.addressLine2}</div>}
                    </Stack>
                  </Grid>
                  <Grid item xs className="my-auto">
                    <div>{school.postalCode}</div>
                  </Grid>
                  <Grid item xs className="my-auto">
                    <div>{school.city}</div>
                  </Grid>
                  <Grid item xs={1} className="my-auto">
                    <div className="d-flex gap-1 flex-wrap">
                      <Link to={`/school/show/${school.guid}`}>
                        <Tippy content={t("showSchool")} arrow={true} animation={"scale"}>
                          <Button variant="outlined">
                            <FontAwesomeIcon icon={faWindowMaximize} />
                          </Button>
                        </Tippy>
                      </Link>
                      <Tippy content={t("removeSchool")} arrow={true} animation={"scale"}>
                        <Button variant="outlined" color="error" onClick={() => removeSchoolClickHandler(school.guid)}>
                          <FontAwesomeIcon icon={faTrash} />
                        </Button>
                      </Tippy>
                    </div>
                  </Grid>
                </Grid>
              </ListItem>
            ))}
            {props.schoolsList?.length == 0 && (
              <Row className="text-center">
                <div>{t("noSchoolsManaged")}</div>
              </Row>
            )}
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(
  (state: any) => ({
    userId: getCurrentUserIdReduxProxy(state),
    schoolsList: getSchoolsListReduxProxy(state),
    currentSchoolGuid: getCurrentSchoolReduxProxy(state)?.schoolGuid,
    currentPerson: getCurrentPersonReduxProxy(state) ?? undefined,
  }),
  (dispatch: any) => ({
    setSchoolsList: (action: setSchoolsListAction) => setSchoolsListReduxWrapper(dispatch, action),
    setCurrentSchool: (action: setSchoolAction) => setSchoolReduxWrapper(dispatch, action),
  })
)(SchoolsList);
