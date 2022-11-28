import { Button } from '@mui/material';
import { Stack } from '@mui/system';
import moment from 'moment';
import React, { ReactElement, useState } from 'react';
import { useTranslation } from 'react-i18next';
import ClassResponse from '../../../../ApiClient/Schools/Definitions/Responses/ClassResponse';
import InfiniteScrollWrapper from '../../../Shared/InfiniteScrollWrapper';
import AddClassModal from './AddClassModal';
import { connect } from 'react-redux';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Tippy from '@tippyjs/react';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';
import Notifications from '../../../../Notifications/Notifications';
import ClassesProxy from '../../../../ApiClient/Classes/ClassesProxy';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import PermissionsBlocker from '../../../Shared/PermissionsBlocker';
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';
import { Table } from 'react-bootstrap';

interface Props {
  currentSchool: any;
}

function ManageClasses(props: Props): ReactElement {
  const { t } = useTranslation('classes');
  const [showAddClassModal, setShowAddClassModal] = useState(false);
  const [refreshKey, setRefreshKey] = useState(0);
  const navigate = useNavigate();
  const removeClassClickHandler = async (classGuid: string): Promise<void> => {
    await Swal.fire({
      title: t('removeClass'),
      text: t('youSureRemoveClass'),
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: t('yes'),
      denyButtonText: t('no'),
      icon: 'warning',
    }).then((result) => {
      if (result.isConfirmed) {
        ClassesProxy.removeClass(classGuid)
          .then(() => {
            Notifications.showSuccessNotification(
              'classRemovedNotificationTitle',
              'classRemovedNotificationText'
            );
            setRefreshKey((k) => k + 1);
          })
          .catch(Notifications.showApiError);
      }
    });
  };
  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <h5 className="my-auto">{t('classes')}</h5>
          <div>
            <PermissionsBlocker
              allowingPermissions={[
                PermissionLevelEnum.Classes_CanManageOwn,
                PermissionLevelEnum.Classes_CanManageAll,
              ]}
            >
              <Button
                onClick={() => setShowAddClassModal(true)}
                variant="outlined"
              >
                {t('addClasses')}
              </Button>
              <AddClassModal
                show={showAddClassModal}
                onHide={() => setShowAddClassModal(false)}
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
                    <th>{t('description')}</th>
                    <th>{t('createdDate')}</th>
                    <th>{t('actions')}</th>
                  </tr>
                </thead>
                <tbody>{items}</tbody>
              </Table>
            )}
            mapper={(element: ClassResponse, index) => (
              <tr
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/class/show/${element.guid}`);
                }}
                key={index}
              >
                <td>{element.name}</td>
                <td>{element.description}</td>
                <td>{moment.utc(element.createdDate).local().format('L')}</td>
                <td className="d-flex gap-1 flex-wrap">
                  <PermissionsBlocker
                    allowingPermissions={[
                      PermissionLevelEnum.Classes_CanManageOwn,
                      PermissionLevelEnum.Classes_CanManageAll,
                    ]}
                  >
                    <Tippy
                      content={t('removeClass')}
                      arrow={true}
                      animation={'scale'}
                    >
                      <Button
                        variant="outlined"
                        color="error"
                        onClick={async (evt) => {
                          evt.stopPropagation();
                          await removeClassClickHandler(element.guid);
                        }}
                      >
                        <FontAwesomeIcon icon={faTrash} />
                      </Button>
                    </Tippy>
                  </PermissionsBlocker>
                </td>
              </tr>
            )}
            fetch={async (page: number) => {
              if (!props.currentSchool.schoolGuid) return [];
              const resp = await SchoolsProxy.getClassesInSchool(
                props.currentSchool.schoolGuid,
                page
              );
              return resp.data as [];
            }}
            effect={[
              props.currentSchool.schoolGuid,
              showAddClassModal,
              refreshKey,
            ]}
          />
        </Stack>
      </Stack>
    </div>
  );
}

export default connect(
  (state: any) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
  }),
  () => ({})
)(ManageClasses);
