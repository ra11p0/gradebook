import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
import AddNewStudentModal from "./AddNewStudentModal";
import { Grid, List, ListItem, Stack } from "@mui/material";
import moment from "moment";
import Notifications from "../../../../Notifications/Notifications";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import StudentInSchoolResponse from "../../../../ApiClient/Schools/Definitions/StudentInSchoolResponse";
import InfiniteScroll from "react-infinite-scroll-component";
import InfinireScrollWrapper from "../../../Shared/InfinireScrollWrapper";
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
  const [studentsInSchool, setStudentsInSchool] = useState(
    [] as StudentInSchoolResponse[]
  );
  useEffect(() => {
    /*SchoolsProxy.getStudentsInSchool(props.currentSchoolGuid!)
      .then((response) => {
        setStudentsInSchool(response.data);
      })
      .catch((error) => {
        Notifications.showApiError(error);
      });*/
  }, [showAddStudentModal]);

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
            <Button onClick={() => setShowAddStudentModal(true)}>
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
          </Grid>
        </Stack>
        <Stack>
          <List>
            {/*studentsInSchool.map((element, index) => (
              <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                <Grid container spacing={2}>
                  <Grid item xs>
                    <div>{element.name}</div>
                  </Grid>
                  <Grid item xs>
                    <div>{element.surname}</div>
                  </Grid>
                  <Grid item xs>
                    <div>{moment(element.birthday).format("YYYY-MM-DD")}</div>
                  </Grid>
                  <Grid item xs>
                    <div>
                      <FontAwesomeIcon
                        icon={element.isActive ? faCheck : faTimes}
                      />
                    </div>
                  </Grid>
                </Grid>
              </ListItem>
            ))*/}
            <InfinireScrollWrapper
              mapper={(element: StudentInSchoolResponse, index) => (
                <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                  <Grid container spacing={2}>
                    <Grid item xs>
                      <div>{element.name}</div>
                    </Grid>
                    <Grid item xs>
                      <div>{element.surname}</div>
                    </Grid>
                    <Grid item xs>
                      <div>{moment(element.birthday).format("YYYY-MM-DD")}</div>
                    </Grid>
                    <Grid item xs>
                      <div>
                        <FontAwesomeIcon
                          icon={element.isActive ? faCheck : faTimes}
                        />
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
              effect={[props.currentSchoolGuid, showAddStudentModal]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(StudentsList);
