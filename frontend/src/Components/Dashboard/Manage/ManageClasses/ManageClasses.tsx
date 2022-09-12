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

const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
});
type Props = {
  currentSchoolGuid?: string;
};

function ManageClasses(props: Props) {
  const { t } = useTranslation("manageClasses");
  const [showAddClassModal, setShowAddClassModal] = useState(false);
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
        <Stack>
          <Grid container spacing={2}>
            <Grid item xs>
              <div>{t("invitationCode")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("isUsed")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("exprationDate")}</div>
            </Grid>
            <Grid item xs>
              <div>{t("person")}</div>
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
              effect={[props.currentSchoolGuid, showAddClassModal]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(mapStateToProps, () => {})(ManageClasses);
