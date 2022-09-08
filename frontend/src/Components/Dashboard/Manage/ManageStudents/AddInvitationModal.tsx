import React, { ReactElement, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Modal } from 'react-bootstrap';
import { Button, FormControl, ListItemText, MenuItem, OutlinedInput, Select, Stack } from '@mui/material';
import StudentResponse from '../../../../ApiClient/Students/Definitions/StudentResponse';
import StudentsProxy from '../../../../ApiClient/Students/StudentsProxy';
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
                    <div>
                        {t('selectPeopleToInvite')}
                    </div>
                    <div>
                        <FormControl>
                            <Select
                                //labelId="demo-multiple-checkbox-label"
                                //id="demo-multiple-checkbox"
                                multiple
                                value={selectedStudents}
                                onChange={(event) => {
                                    const { target: { value } } = event;
                                    setSelectedStudents(typeof value === 'string' ? value.split(',') : value);
                                }}
                                //input={<OutlinedInput label="Tag" />}
                                renderValue={(selected) => selected.length}
                            //MenuProps={MenuProps}
                            >
                                {inactiveStudents.map((student) => (
                                    <MenuItem key={student.guid} value={student.guid}>
                                        <ListItemText primary={`${student.name} ${student.surname}`} />
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </div>
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