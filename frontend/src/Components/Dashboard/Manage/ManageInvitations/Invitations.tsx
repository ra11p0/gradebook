import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
import AddInvitationModal from "./AddInvitationModal";
import InvitationResponse from "../../../../ApiClient/Invitations/Definitions/Responses/InvitationResponse";
import { Stack, Grid, List, ListItem } from "@mui/material";
import InfiniteScrollWrapper from "../../../Shared/InfiniteScrollWrapper";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import moment from "moment";
import Person from "../../../Shared/Person";
import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import getCurrentSchoolReduxProxy from "../../../../Redux/ReduxProxy/getCurrentSchoolReduxProxy";
import PermissionsBlocker from "../../../Shared/PermissionsBlocker"
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';
const mapStateToProps = (state: any) => ({
  currentSchool: getCurrentSchoolReduxProxy(state),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface InvitationsProps {
  currentSchool: any;
}
const Invitations = (props: InvitationsProps): ReactElement => {
  const [showInvitationModal, setShowInvitationModal] = useState(false);
  const { t } = useTranslation("invitations");
  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("invitations")}</div>
          <div>
            <PermissionsBlocker permissions={[PermissionLevelEnum.Invitations_CanInvite]}>
              <Button className="addInvitationButton" onClick={() => setShowInvitationModal(true)}>
                {t("inviteStudent")}
              </Button>
              <AddInvitationModal show={showInvitationModal} onHide={() => setShowInvitationModal(false)} />
            </PermissionsBlocker>
          </div>
        </div>
        <Stack className={"border rounded-3 my-1 p-3 bg-light"}>
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
              mapper={(invitation: InvitationResponse, index) => (
                <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                  <Grid container spacing={1}>
                    <Grid item xs className="my-auto">
                      {invitation.invitationCode}
                    </Grid>
                    <Grid item xs className="my-auto">
                      <FontAwesomeIcon icon={invitation.isUsed ? faCheck : faTimes} />
                    </Grid>
                    <Grid item xs className="my-auto">
                      {moment(invitation.exprationDate).format("YYYY-MM-DD HH:MM")}
                    </Grid>
                    {invitation.invitedPerson && (
                      <Grid item xs className="my-auto">
                        <Person
                          guid={invitation.invitedPersonGuid ?? ""}
                          name={invitation.invitedPerson.name}
                          surname={invitation.invitedPerson.surname}
                          birthday={invitation.invitedPerson.birthday}
                        />
                      </Grid>
                    )}
                  </Grid>
                </ListItem>
              )}
              fetch={async (page: number) => {
                if (!props.currentSchool.schoolGuid) return [];
                let response = await SchoolsProxy.getInvitationsInSchool(props.currentSchool.schoolGuid!, page);
                return response.data as [];
              }}
              effect={[showInvitationModal, props.currentSchool.schoolGuid]}
            />
          </List>
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(Invitations);
