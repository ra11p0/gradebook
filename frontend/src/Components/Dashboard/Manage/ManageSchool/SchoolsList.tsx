import React, { ReactElement, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import Swal from 'sweetalert2';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../Notifications/Notifications';
import AccountProxy from '../../../../ApiClient/Accounts/AccountsProxy';
import getCurrentUserIdReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';
import getSchoolsListReduxProxy from '../../../../Redux/ReduxQueries/account/getSchoolsListRedux';
import GetSchoolResponse from '../../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse';
import setSchoolsListReduxWrapper, {
  setSchoolsListAction,
} from '../../../../Redux/ReduxCommands/account/setSchoolsListRedux';
import setSchoolReduxWrapper, {
  setSchoolAction,
} from '../../../../Redux/ReduxCommands/account/setSchoolRedux';
import JoinSchoolModal from './JoinSchoolModal';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import getCurrentPersonReduxProxy, {
  CurrentPersonProxyResult,
} from '../../../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import { Row, Stack, Table } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import Tippy from '@tippyjs/react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { Button } from '@mui/material';

interface SchoolsListProps {
  userId?: string;
  schoolsList: GetSchoolResponse[] | null;
  currentSchoolGuid?: string;
  setSchoolsList?: (action: setSchoolsListAction) => void;
  setCurrentSchool?: (action: setSchoolAction) => void;
  currentPerson?: CurrentPersonProxyResult;
}

function SchoolsList(props: SchoolsListProps): ReactElement {
  const { t } = useTranslation('schoolsList');
  const [showJoinSchoolModal, setShowJoinSchoolModal] = useState(false);
  const [refreshEffectKey, setRefreshEffectKey] = useState(0);
  const navigate = useNavigate();
  useEffect(() => {
    if (!props.userId) return;
    void AccountProxy.getAccessibleSchools(props.userId)
      .then((schoolsResponse) => {
        if (!props.currentSchoolGuid) return;
        if (
          !schoolsResponse.data
            .map((e) => e.school.guid)
            .includes(props.currentSchoolGuid)
        ) {
          if (!props.setCurrentSchool) return;
          if (schoolsResponse.data.length !== 0)
            props.setCurrentSchool({
              schoolGuid: schoolsResponse.data[0].school.guid,
              schoolName: schoolsResponse.data[0].school.name,
            });
          else props.setCurrentSchool({ schoolName: '', schoolGuid: '' });
        }
      })
      .catch(Notifications.showApiError);
  }, [showJoinSchoolModal, refreshEffectKey]);

  async function removeSchoolClickHandler(schoolGuid: string): Promise<void> {
    await Swal.fire({
      title: t('removeSchool'),
      text: t('youSureRemoveSchool'),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t('yes'),
      denyButtonText: t('no'),
      icon: 'warning',
    }).then((result) => {
      if (result.isConfirmed) {
        SchoolsProxy.removeSchool(schoolGuid)
          .then((response) => {
            Notifications.showSuccessNotification(
              'schoolRemovedNotificationTitle',
              'schoolRemovedNotificationText'
            );
            setRefreshEffectKey((k) => k + 1);
          })
          .catch((err) => {
            Notifications.showApiError(err);
          });
      }
    });
  }

  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <h5 className="my-auto">{t('managedSchools')}</h5>
          <div className="d-flex gap-2">
            <JoinSchoolModal
              show={showJoinSchoolModal}
              onHide={() => {
                setShowJoinSchoolModal(false);
              }}
              person={props.currentPerson}
            />
            <Button
              className="addSchoolButton"
              onClick={() => setShowJoinSchoolModal(true)}
              variant={'outlined'}
            >
              {t('addSchool')}
            </Button>
          </div>
        </div>
        <Stack>
          <Table striped bordered hover responsive>
            <thead>
              <tr>
                <th>{t('name')}</th>
                <th>{t('address')}</th>
                <th>{t('postalCode')}</th>
                <th>{t('city')}</th>
                <th>{t('actions')}</th>
              </tr>
            </thead>
            <tbody>
              {props.schoolsList?.map((school, index) => (
                <tr
                  className="cursor-pointer"
                  onClick={() => {
                    navigate(`/school/show/${school.guid}`);
                  }}
                  key={index}
                >
                  <td>{school.name}</td>
                  <td>
                    <div>{school.addressLine1}</div>
                    {school.addressLine2 && <div>{school.addressLine2}</div>}
                  </td>
                  <td>{school.postalCode}</td>
                  <td>{school.city}</td>
                  <td>
                    <div className="d-flex gap-1 flex-wrap">
                      <Tippy
                        content={t('removeSchool')}
                        arrow={true}
                        animation={'scale'}
                      >
                        <Button
                          variant="outlined"
                          color="error"
                          onClick={async (evt) => {
                            evt.stopPropagation();
                            await removeSchoolClickHandler(school.guid);
                          }}
                        >
                          <FontAwesomeIcon icon={faTrash} />
                        </Button>
                      </Tippy>
                    </div>
                  </td>
                </tr>
              ))}
              {props.schoolsList?.length === 0 && (
                <Row className="text-center">
                  <div>{t('noSchoolsManaged')}</div>
                </Row>
              )}
            </tbody>
          </Table>
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(
  (state: any) => ({
    userId: getCurrentUserIdReduxProxy(state),
    schoolsList: getSchoolsListReduxProxy(state),
    currentSchoolGuid: getCurrentSchoolReduxProxy(state)?.schoolGuid,
    currentPerson: getCurrentPersonReduxProxy(state) ?? undefined,
  }),
  (dispatch: any) => ({
    setSchoolsList: (action: setSchoolsListAction) =>
      setSchoolsListReduxWrapper(dispatch, action),
    setCurrentSchool: async (action: setSchoolAction) =>
      await setSchoolReduxWrapper(dispatch, action),
  })
)(SchoolsList);
