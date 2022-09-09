import React, { ReactElement, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Modal } from 'react-bootstrap';
import { Button, FormControl, InputLabel, MenuItem, OutlinedInput, Select, Stack } from '@mui/material';
import StudentResponse from '../../../../ApiClient/Students/Definitions/StudentResponse';
import StudentsProxy from '../../../../ApiClient/Students/StudentsProxy';
import Person from '../../../Shared/Person';
import PersonSmall from '../../../Shared/PersonSmall';
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface AddInvitationModalProps {
    show: boolean;
    onHide: () => void;
}
const AddInvitationModal = (props: AddInvitationModalProps): ReactElement => {
    const { t } = useTranslation('addInvitationModal');
    const [inactiveStudents, setInactiveStudents] = useState([] as StudentResponse[]);
    const [selectedStudents, setSelectedStudents] = useState<string[]>([]);

    useEffect(() => {
        StudentsProxy.getInactiveAccessibleStudents()
            .then(response => {
                setInactiveStudents(response.data);
            })
    }, []);
    return (
        <Modal show={props.show} onHide={props.onHide}>
            <Modal.Header closeButton>
                <Modal.Title>{t('addInvitation')}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Stack>
                    <Stack>
                        <FormControl>
                            <InputLabel>
                                {t('selectPeopleToInvite')}
                            </InputLabel>
                            <Select
                                multiple
                                value={selectedStudents}
                                onChange={(event) => {
                                    const { target: { value } } = event;
                                    setSelectedStudents(typeof value === 'string' ? value.split(',') : value);
                                }}
                                renderValue={(selected) => selected.length}
                                label={t('selectPeopleToInvite')}
                            >
                                {inactiveStudents.map((student) => (
                                    <MenuItem key={student.guid} value={student.guid}
                                        className='row'>
                                        <Person
                                            name={student.name}
                                            surname={student.surname}
                                            birthday={student.birthday}
                                        />
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Stack>
                    <Stack>
                        <div className='d-flex flex-wrap justify-content-center'>
                            <>
                                {
                                    inactiveStudents.filter(student => selectedStudents.includes(student.guid)).map(student =>
                                        <PersonSmall
                                            name={student.name}
                                            surname={student.surname}
                                            key={student.guid}
                                        />
                                    )
                                }
                            </>
                        </div>
                    </Stack>
                </Stack>

            </Modal.Body>
            <Modal.Footer>
                <Button variant='outlined'>
                    {t('addInvitation')}
                </Button>
            </Modal.Footer>
        </Modal>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(AddInvitationModal);