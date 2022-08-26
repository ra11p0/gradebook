import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import AddInvitationModal from '../AddInvitationModal';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface InvitationsProps{ }
const Invitations = (props: InvitationsProps): ReactElement => {
    const [showInvitationModal, setShowInvitationModal] = useState(false);
    const {t} = useTranslation();
    return (
        <div>
            Invitations
            <Button onClick={()=>setShowInvitationModal(true)}>Invite student</Button>
            <AddInvitationModal show={showInvitationModal} onHide={()=>setShowInvitationModal(false)}/>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(Invitations);