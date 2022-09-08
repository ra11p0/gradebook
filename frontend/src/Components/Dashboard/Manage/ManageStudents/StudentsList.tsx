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
                <Stack>
                    <List>
                        {
                            accessibleStudents.map((element, index) =>
                                <ListItem key={index}>
                                    <Grid container columnGap={1}>
                                        <Grid>
                                            {element.name}
                                        </Grid>
                                        <Grid>
                                            {element.surname}
                                        </Grid>
                                        <Grid>
                                            {moment(element.birthday).format('YYYY-MM-DD')}
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