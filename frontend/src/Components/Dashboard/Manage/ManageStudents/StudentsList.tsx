import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Stack, Button } from '@mui/material';
import moment from 'moment';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faTimes, faTrash } from '@fortawesome/free-solid-svg-icons';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import StudentInSchoolResponse from '../../../../ApiClient/Schools/Definitions/Responses/StudentInSchoolResponse';
import InfiniteScrollWrapper from '../../../Shared/InfiniteScrollWrapper';
import Tippy from '@tippyjs/react';
import { useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
import Notifications from '../../../../Notifications/Notifications';
import PeopleProxy from '../../../../ApiClient/People/PeopleProxy';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import AddNewStudentModalWithButton from './AddNewStudentModalWithButton';
import PermissionsBlocker from '../../../Shared/PermissionsBlocker';
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';
import { Table } from 'react-bootstrap';
import { GlobalState } from '../../../../store';

interface StudentsListProps {
  currentSchoolGuid: string | undefined;
}
function StudentsList(props: StudentsListProps): React.ReactElement {
  const { t } = useTranslation('studentsList');
  const [showAddStudentModal, setShowAddStudentModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);

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
          .then(() => {
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
          <h5 className="my-auto">{t('studentsList')}</h5>
          <div>
            <PermissionsBlocker
              allowingPermissions={[
                PermissionLevelEnum.Students_CanCreateAndDelete,
              ]}
            >
              <AddNewStudentModalWithButton
                setShowAddStudentModal={setShowAddStudentModal}
                showAddStudentModal={showAddStudentModal}
              />
            </PermissionsBlocker>
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
            mapper={(element: StudentInSchoolResponse, index) => (
              <StudentRow
                {...element}
                removePersonClickHandler={removePersonClickHandler}
                key={index}
              />
            )}
            fetch={async (page: number) => {
              if (!props.currentSchoolGuid) return [];
              const resp = await SchoolsProxy.getStudentsInSchool(
                props.currentSchoolGuid,
                page
              );
              return resp.data as [];
            }}
            effect={[
              props.currentSchoolGuid,
              showAddStudentModal,
              refreshEffectKey,
            ]}
          />
        </Stack>
      </Stack>
    </div>
  );
}

function StudentRow(
  element: StudentInSchoolResponse & {
    removePersonClickHandler: (guid: string) => void;
  }
): ReactElement {
  const navigate = useNavigate();
  return (
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
        <FontAwesomeIcon icon={element.isActive ? faCheck : faTimes} />
      </td>
      <td>
        <StudentActions {...element} />
      </td>
    </tr>
  );
}

function StudentActions(
  element: StudentInSchoolResponse & {
    removePersonClickHandler: (guid: string) => void;
  }
): ReactElement {
  const { t } = useTranslation('studentsList');
  return (
    <div className="d-flex gap-1 flex-wrap">
      <PermissionsBlocker
        allowingPermissions={[PermissionLevelEnum.Students_CanCreateAndDelete]}
      >
        <Tippy content={t('removePerson')} arrow={true} animation={'scale'}>
          <Button
            variant="outlined"
            color="error"
            onClick={(evt) => {
              evt.stopPropagation();
              element.removePersonClickHandler(element.guid);
            }}
          >
            <FontAwesomeIcon icon={faTrash} />
          </Button>
        </Tippy>
      </PermissionsBlocker>
    </div>
  );
}

export default connect(
  (state: GlobalState) => ({
    currentSchoolGuid: getCurrentSchoolReduxProxy(state)?.schoolGuid,
  }),
  () => ({})
)(StudentsList);
