import { Button, Grid, List, ListItem, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import AddNewSchoolModal from "./AddNewSchoolModal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import Tippy from "@tippyjs/react";
import Swal from "sweetalert2";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../../../Notifications/Notifications";
import { Row } from "react-bootstrap";
import { setSchoolsListAction, setSchoolsListWrapper } from "../../../../ReduxWrappers/setSchoolsListWrapper";
import { setSchoolAction, setSchoolWrapper } from "../../../../ReduxWrappers/setSchoolWrapper";
import AccountProxy from "../../../../ApiClient/Account/AccountProxy";
import { currentUserIdProxy } from "../../../../ReduxProxy/currentUserIdProxy";
import { schoolsListProxy } from "../../../../ReduxProxy/schoolsListProxy";
import GetSchoolResponse from "../../../../ApiClient/Schools/Definitions/GetSchoolResponse";

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolsList: (action: setSchoolsListAction) => setSchoolsListWrapper(dispatch, action),
  setCurrentSchool: (action: setSchoolAction) => setSchoolWrapper(dispatch, action),
});
const mapStateToProps = (state: any) => ({
  userId: currentUserIdProxy(state),
  schoolsList: schoolsListProxy(state),
  currentSchoolGuid: state.common.school?.schoolGuid,
});

interface SchoolsListProps {
  userId?: string;
  schoolsList: GetSchoolResponse[] | null;
  currentSchoolGuid?: string;
  setSchoolsList?: (action: setSchoolsListAction) => void;
  setCurrentSchool?: (action: setSchoolAction) => void;
}

function SchoolsList(props: SchoolsListProps) {
  const { t } = useTranslation("schoolsList");
  const [showAddSchoolModal, setShowAddSchoolModal] = useState(false);
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
  }, [showAddSchoolModal, refreshEffectKey]);

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
          <div>
            <AddNewSchoolModal
              show={showAddSchoolModal}
              onHide={() => {
                setShowAddSchoolModal(false);
              }}
            />
            <Button onClick={() => setShowAddSchoolModal(true)} variant={"outlined"}>
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

export default connect(mapStateToProps, mapDispatchToProps)(SchoolsList);
