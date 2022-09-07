import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button, Stack } from 'react-bootstrap';
import AddInvitationModal from '../AddInvitationModal';
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface InvitationsProps { }
const Invitations = (props: InvitationsProps): ReactElement => {
    const [showInvitationModal, setShowInvitationModal] = useState(false);
    const { t } = useTranslation();
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
            </Stack>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(Invitations);