import React, { ReactElement, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import AddNewStudentModal from './AddNewStudentModal';
import { Grid, List, ListItem, Stack } from '@mui/material';
import StudentsProxy from '../../../../ApiClient/Students/StudentsProxy';
import StudentResponse from '../../../../ApiClient/Students/Definitions/StudentResponse';
import moment from 'moment';
import Notifications from '../../../../Notifications/Notifications';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faTimes } from '@fortawesome/free-solid-svg-icons';
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface StudentsListProps { }
const StudentsList = (props: StudentsListProps): ReactElement => {
    const { t } = useTranslation('studentsList');
    const [showAddStudentModal, setShowAddStudentModal] = useState(false);
    const [accessibleStudents, setAccessibleStudents] = useState([] as StudentResponse[]);
    useEffect(() => {
        StudentsProxy.getAccessibleStudents()
            .then(response => {
                setAccessibleStudents(response.data);
            })
            .catch(error => {
                Notifications.showError(error.response.data);
            });
    }, [showAddStudentModal]);

    return (
        <div>
            <Stack>
                <div className='d-flex justify-content-between'>
                    <div className='my-auto'>
                        {t('studentsList')}
                    </div>
                    <div>
                        <AddNewStudentModal show={showAddStudentModal} onHide={() => {
                            setShowAddStudentModal(false);
                        }} />
                        <Button onClick={() => setShowAddStudentModal(true)}>
                            {t('addStudent')}
                        </Button>
                    </div>
                </div>
                <Stack className={'border rounded-3 my-1 p-3 bg-light'}>
                    <Grid container spacing={2} >
                        <Grid item xs>
                            <div>
                                {t('name')}
                            </div>
                        </Grid>
                        <Grid item xs>
                            <div>
                                {t('surname')}
                            </div>
                        </Grid>
                        <Grid item xs>
                            <div>
                                {t('birthday')}
                            </div>
                        </Grid>
                        <Grid item xs>
                            <div>
                                {t('isActive')}
                            </div>
                        </Grid>
                    </Grid>
                </Stack>
                <Stack>
                    <List>
                        {
                            accessibleStudents.map((element, index) =>
                                <ListItem key={index} className={'border rounded-3 my-1 p-3'}>
                                    <Grid container spacing={2}>
                                        <Grid item xs>
                                            <div>
                                                {element.name}
                                            </div>
                                        </Grid>
                                        <Grid item xs>
                                            <div>
                                                {element.surname}
                                            </div>
                                        </Grid>
                                        <Grid item xs>
                                            <div>
                                                {moment(element.birthday).format('YYYY-MM-DD')}
                                            </div>
                                        </Grid>
                                        <Grid item xs>
                                            <div>
                                                <FontAwesomeIcon icon={element.isActive ? faCheck : faTimes} />
                                            </div>
                                        </Grid>
                                    </Grid>
                                </ListItem>
                            )
                        }
                    </List>
                </Stack>
            </Stack>

        </div >
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(StudentsList);