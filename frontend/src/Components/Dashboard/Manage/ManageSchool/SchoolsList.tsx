import { Box, Button, Grid, List, ListItem, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { setSchoolsList } from "../../../../Actions/Account/accountActions";
import GetAccessibleSchoolsResponse from "../../../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
import { Link } from "react-router-dom";
import AddNewSchoolModal from "./AddNewSchoolModal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import Tippy from "@tippyjs/react";
import Swal from "sweetalert2";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../../../Notifications/Notifications";

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolsList: (schoolsList: GetAccessibleSchoolsResponse[]) => {
    dispatch({ ...setSchoolsList, schoolsList });
  },
});
const mapStateToProps = (state: any) => ({
  personGuid: state.common.session?.personGuid,
  schoolsList: state.common.schoolsList,
});

interface SchoolsListProps {
  personGuid?: string;
  schoolsList?: GetAccessibleSchoolsResponse[];
  setSchoolsList?: (schoolsList: GetAccessibleSchoolsResponse[]) => void;
}

function SchoolsList(props: SchoolsListProps) {
  const { t } = useTranslation("schoolsList");
  const [showAddSchoolModal, setShowAddSchoolModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);

  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.personGuid!).then(
      (schoolsResponse) => {
        props.setSchoolsList!(schoolsResponse.data);
      }
    );
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
            Notifications.showSuccessNotification(
              "schoolRemovedNotificationTitle",
              "schoolRemovedNotificationText"
            );
            setRefreshEffectKey((k) => k + 1);
          })
          .catch((err) => {
            Notifications.showError(err.data ?? err.message);
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
            <Button
              onClick={() => setShowAddSchoolModal(true)}
              variant={"outlined"}
            >
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
                        <Tippy
                          content={t("showSchool")}
                          arrow={true}
                          animation={"scale"}
                        >
                          <Button variant="outlined">
                            <FontAwesomeIcon icon={faWindowMaximize} />
                          </Button>
                        </Tippy>
                      </Link>
                      <Tippy
                        content={t("removeSchool")}
                        arrow={true}
                        animation={"scale"}
                      >
                        <Button
                          variant="outlined"
                          color="error"
                          onClick={() => removeSchoolClickHandler(school.guid)}
                        >
                          <FontAwesomeIcon icon={faTrash} />
                        </Button>
                      </Tippy>
                    </div>
                  </Grid>
                </Grid>
              </ListItem>
            ))}
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(SchoolsList);
