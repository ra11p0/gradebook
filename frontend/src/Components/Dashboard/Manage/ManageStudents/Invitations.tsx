import React, { ReactElement, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import AddInvitationModal from './AddInvitationModal';
import InvitationsProxy from '../../../../ApiClient/Invitations/InvitationsProxy';
import InvitationResponse from '../../../../ApiClient/Invitations/Definitions/InvitationResponse';
import { Stack, Grid, List, ListItem } from '@mui/material';
import moment from 'moment';
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface InvitationsProps { }
const Invitations = (props: InvitationsProps): ReactElement => {
    const [showInvitationModal, setShowInvitationModal] = useState(false);
    const [invitations, setInvitations] = useState([] as InvitationResponse[]);
    const { t } = useTranslation('invitations');
    useEffect(() => {
        InvitationsProxy.getUsersInvitations()
            .then(response => {
                setInvitations(response.data);
            });
    }, [showInvitationModal]);
    return (
        <div>
            <Stack>
                <div className='d-flex justify-content-between'>
                    <div className='my-auto'>
                        {t('invitations')}
                    </div>
                    <div>
                        <Button onClick={() => setShowInvitationModal(true)}>{t('inviteStudent')}</Button>
                        <AddInvitationModal show={showInvitationModal} onHide={() => setShowInvitationModal(false)} />
                    </div>
                </div>
                <Stack>
                    <List>
                        {
                            invitations.map((element, index) =>
                                <ListItem key={index}>
                                    <Grid container columnGap={1}>
                                        <Grid>
                                            {element.invitationCode}
                                        </Grid>
                                        <Grid>
                                            {element.isUsed.toString()}
                                        </Grid>
                                        <Grid>
                                            {moment(element.exprationDate).format('YYYY-MM-DD')}
                                        </Grid>
                                    </Grid>
                                </ListItem>
                            )
                        }
                    </List>
                </Stack>
            </Stack>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(Invitations);