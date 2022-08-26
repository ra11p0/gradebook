import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button, Modal } from 'react-bootstrap';
import AddNewStudentModal from './AddNewStudentModal';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface StudentsListProps{ }
const StudentsList = (props: StudentsListProps): ReactElement => {
    const {t} = useTranslation();
    const [showAddStudentModal, setShowAddStudentModal] = useState(false);
    return (
        <div>
            <Button onClick={()=>setShowAddStudentModal(true)}>
                AddNewStudent
            </Button>
            <AddNewStudentModal show={showAddStudentModal} onHide={()=>setShowAddStudentModal(false)}/>
            StudentsList
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(StudentsList);