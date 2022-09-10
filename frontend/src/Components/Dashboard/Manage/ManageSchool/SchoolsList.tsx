import { Button, Grid, List, ListItem, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
import AddNewSchoolModal from "./AddNewSchoolModal";

const mapDispatchToProps = (dispatch: any) => ({});
const mapStateToProps = (state: any) => ({
  personGuid: state.common.session?.personGuid,
});

interface SchoolsListProps {
  personGuid?: string;
}

function SchoolsList(props: SchoolsListProps) {
  const { t } = useTranslation();
  const [schools, setSchools] = useState(null);
  const [showAddSchoolModal, setShowAddSchoolModal] = useState(false);

  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.personGuid!);
  }, []);

  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("managed schools")}</div>
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
            {/* {accessibleStudents.map((element, index) => (
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
            ))} */}
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(SchoolsList);
