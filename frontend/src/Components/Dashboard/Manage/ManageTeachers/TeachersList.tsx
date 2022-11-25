import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import Swal from 'sweetalert2';
import { Button, Stack } from '@mui/material';
import AddNewTeacherModal from './AddNewTeacherModal';
import InfiniteScrollWrapper from '../../../Shared/InfiniteScrollWrapper';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Tippy from '@tippyjs/react';
import { faCheck, faTimes, faTrash } from '@fortawesome/free-solid-svg-icons';
import PeopleProxy from '../../../../ApiClient/People/PeopleProxy';
import Notifications from '../../../../Notifications/Notifications';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import TeacherInSchoolResponse from '../../../../ApiClient/Schools/Definitions/Responses/TeacherInSchoolResponse';
import moment from 'moment';
import { useNavigate } from 'react-router-dom';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import { Table } from 'react-bootstrap';

interface ManageTeachersProps {
  currentSchool: any;
}
const TeachersList = (props: ManageTeachersProps): ReactElement => {
  const { t } = useTranslation('manageTeachers');
  const [showAddTeacherModal, setShowAddTeacherModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);
  const navigate = useNavigate();
  const removePersonClickHandler = async (
    personGuid: string
  ): Promise<void> => {
    await Swal.fire({
      title: t('removePerson'),
      text: t('youSureRemovePerson'),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t('yes'),
      denyButtonText: t('no'),
      icon: 'warning',
    }).then((result) => {
      if (result.isConfirmed) {
        PeopleProxy.removePerson(personGuid)
          .then((response) => {
            Notifications.showSuccessNotification(
              'personRemovedNotificationTitle',
              'personRemovedNotificationText'
            );
            setRefreshEffectKey((k) => k + 1);
          })
          .catch(Notifications.showApiError);
      }
    });
  };

  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <h5 className="my-auto">{t('teachersList')}</h5>
          <div>
            <AddNewTeacherModal
              show={showAddTeacherModal}
              onHide={() => {
                setShowAddTeacherModal(false);
              }}
            />
            <Button
              onClick={() => setShowAddTeacherModal(true)}
              variant="outlined"
            >
              {t('addTeacher')}
            </Button>
          </div>
        </div>
        <Stack>
          <InfiniteScrollWrapper
            wrapper={(items) => (
              <Table striped bordered hover responsive>
                <thead>
                  <tr>
                    <th>{t('name')}</th>
                    <th>{t('surname')}</th>
                    <th>{t('birthday')}</th>
                    <th>{t('isActive')}</th>
                    <th>{t('actions')}</th>
                  </tr>
                </thead>
                <tbody>{items}</tbody>
              </Table>
            )}
            mapper={(element: TeacherInSchoolResponse, index) => (
              <tr
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/person/show/${element.guid}`);
                }}
              >
                <td>{element.name}</td>
                <td>{element.surname}</td>
                <td>{moment.utc(element.birthday).local().format('L')}</td>
                <td>
                  <FontAwesomeIcon
                    icon={element.isActive ? faCheck : faTimes}
                  />
                </td>
                <td>
                  <div className="d-flex gap-1 flex-wrap">
                    <Tippy
                      content={t('removePerson')}
                      arrow={true}
                      animation={'scale'}
                    >
                      <Button
                        variant="outlined"
                        color="error"
                        onClick={async (evt) => {
                          evt.stopPropagation();
                          await removePersonClickHandler(element.guid);
                        }}
                      >
                        <FontAwesomeIcon icon={faTrash} />
                      </Button>
                    </Tippy>
                  </div>
                </td>
              </tr>
            )}
            fetch={async (page: number) => {
              if (!props.currentSchool.schoolGuid) return [];
              const resp = await SchoolsProxy.getTeachersInSchool(
                props.currentSchool.schoolGuid,
                page
              );
              return resp.data as [];
            }}
            effect={[
              props.currentSchool.schoolGuid,
              showAddTeacherModal,
              refreshEffectKey,
            ]}
          />
        </Stack>
      </Stack>
    </div>
  );
};
export default connect(
  (state: any) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
  }),
  () => ({})
)(TeachersList);
