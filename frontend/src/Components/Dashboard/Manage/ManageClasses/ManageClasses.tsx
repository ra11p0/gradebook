import { Button, Grid, List, ListItem } from "@mui/material";
import { Stack } from "@mui/system";
import moment from "moment";
import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import ClassResponse from "../../../../ApiClient/Schools/Definitions/ClassResponse";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import AddClassModal from "./AddClassModal";
import { connect } from "react-redux";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Tippy from "@tippyjs/react";
import { faTrash, faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import Swal from "sweetalert2";
import Notifications from "../../../../Notifications/Notifications";
import ClassesProxy from "../../../../ApiClient/Classes/ClassesProxy";

const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
});
type Props = {
  currentSchoolGuid?: string;
};

function ManageClasses(props: Props) {
  const { t } = useTranslation("classes");
  const [showAddClassModal, setShowAddClassModal] = useState(false);
  const [refreshKey, setRefreshKey] = useState(0);
  const removeClassClickHandler = (classGuid: string) => {
    Swal.fire({
      title: t("removeClass"),
      text: t("youSureRemoveClass"),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t("yes"),
      denyButtonText: t("no"),
      icon: "warning",
    }).then((result) => {
      if (result.isConfirmed) {
        ClassesProxy.removeClass(classGuid)
          .then(() => {
            Notifications.showSuccessNotification(
              "classRemovedNotificationTitle",
              "classRemovedNotificationText"
            );
            setRefreshKey((k) => k + 1);
          })
          .catch(Notifications.showApiError);
      }
    });
  };
  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("classes")}</div>
          <div>
            <Button
              onClick={() => setShowAddClassModal(true)}
              variant="outlined"
            >
              {t("addClasses")}
            </Button>
            <AddClassModal
              show={showAddClassModal}
              onHide={() => setShowAddClassModal(false)}
            />
          </div>
        </div>
        <Stack className={"border rounded-3 my-1 p-3 bg-light"}>
          <Grid container spacing={2}>
            <Grid item xs>
              <div>{t("name")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("description")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("createdDate")}</div>
            </Grid>
            <Grid item xs={1}>
              <div>{t("actions")}</div>
            </Grid>
          </Grid>
        </Stack>
        <Stack>
          <List>
            <InfiniteScrollWrapper
              mapper={(element: ClassResponse, index) => (
                <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                  <Grid container spacing={2}>
                    <Grid item xs>
                      <div>{element.name}</div>
                    </Grid>
                    <Grid item xs>
                      <div>{element.description}</div>
                    </Grid>
                    <Grid item xs>
                      <div>
                        {moment(element.createdDate).format("YYYY-MM-DD")}
                      </div>
                    </Grid>
                    <Grid item xs={1}>
                      <div className="d-flex gap-1 flex-wrap">
                        <Link to={`/class/show/${element.guid}`}>
                          <Tippy
                            content={t("showClass")}
                            arrow={true}
                            animation={"scale"}
                          >
                            <Button variant="outlined">
                              <FontAwesomeIcon icon={faWindowMaximize} />
                            </Button>
                          </Tippy>
                        </Link>
                        <Tippy
                          content={t("removeClass")}
                          arrow={true}
                          animation={"scale"}
                        >
                          <Button
                            variant="outlined"
                            color="error"
                            onClick={() =>
                              removeClassClickHandler(element.guid)
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
                let resp = await SchoolsProxy.getClassesInSchool(
                  props.currentSchoolGuid!,
                  page
                );
                return resp.data as [];
              }}
              effect={[props.currentSchoolGuid, showAddClassModal, refreshKey]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(mapStateToProps, () => ({}))(ManageClasses);
