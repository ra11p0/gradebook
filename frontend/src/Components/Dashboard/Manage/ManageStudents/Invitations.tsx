import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
import AddInvitationModal from "./AddInvitationModal";
import InvitationsProxy from "../../../../ApiClient/Invitations/InvitationsProxy";
import InvitationResponse from "../../../../ApiClient/Invitations/Definitions/InvitationResponse";
import { Stack, Grid, List, ListItem } from "@mui/material";
import moment from "moment";
import Person from "../../../Shared/Person";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface InvitationsProps {}
const Invitations = (props: InvitationsProps): ReactElement => {
  const [showInvitationModal, setShowInvitationModal] = useState(false);
  const [invitations, setInvitations] = useState([] as InvitationResponse[]);
  const { t } = useTranslation("invitations");
  useEffect(() => {
    InvitationsProxy.getUsersInvitations().then((response) => {
      setInvitations(response.data);
    });
  }, [showInvitationModal]);
  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <div className="my-auto">{t("invitations")}</div>
          <div>
            <Button onClick={() => setShowInvitationModal(true)}>
              {t("inviteStudent")}
            </Button>
            <AddInvitationModal
              show={showInvitationModal}
              onHide={() => setShowInvitationModal(false)}
            />
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
            {invitations.map((invitation, index) => (
              <ListItem key={index} className={"border rounded-3 my-1 p-3"}>
                <Grid container spacing={1}>
                  <Grid item xs className="my-auto">
                    {invitation.invitationCode}
                  </Grid>
                  <Grid item xs className="my-auto">
                    <FontAwesomeIcon
                      icon={invitation.isUsed ? faCheck : faTimes}
                    />
                  </Grid>
                  <Grid item xs className="my-auto">
                    {moment(invitation.exprationDate).format(
                      "YYYY-MM-DD HH:MM"
                    )}
                  </Grid>
                  {invitation.invitedPerson && (
                    <Grid item xs className="my-auto">
                      <Person
                        name={invitation.invitedPerson.name}
                        surname={invitation.invitedPerson.surname}
                        birthday={invitation.invitedPerson.birthday}
                      />
                    </Grid>
                  )}
                </Grid>
              </ListItem>
            ))}
          </List>
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(Invitations);
