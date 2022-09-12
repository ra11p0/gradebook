import { Button, Grid, List, ListItem } from "@mui/material";
import { Stack } from "@mui/system";
import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import AddClassModal from "./AddClassModal";

type Props = {};

function ManageClasses({}: Props) {
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
          <List></List>
        </Stack>
      </Stack>
    </div>
  );
}

export default ManageClasses;
