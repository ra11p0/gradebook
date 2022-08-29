import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Modal } from 'react-bootstrap';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface AddInvitationModalProps{ 
    show: boolean;
    onHide: ()=>void;
}
const AddInvitationModal = (props: AddInvitationModalProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <Modal show={props.show} onHide={props.onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Add invitation</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                content
            </Modal.Body>
            <Modal.Footer>
                futer
            </Modal.Footer>
        </Modal>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(AddInvitationModal);