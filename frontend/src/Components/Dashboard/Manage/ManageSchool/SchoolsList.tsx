import { Button, Grid, List, ListItem, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { setSchoolsList } from "../../../../Actions/Account/accountActions";
import GetAccessibleSchoolsResponse from "../../../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import PeopleProxy from "../../../../ApiClient/People/PeopleProxy";
import School from "../../../Shared/School";
import AddNewSchoolModal from "./AddNewSchoolModal";

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
  const { t } = useTranslation();
  const [showAddSchoolModal, setShowAddSchoolModal] = useState(false);

  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.personGuid!).then(
      (schoolsResponse) => {
        props.setSchoolsList!(schoolsResponse.data);
      }
    );
  }, [showAddSchoolModal]);

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
              <div>{t("address")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("postalCode")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("city")}</div>
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
                  <Grid item xs>
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
