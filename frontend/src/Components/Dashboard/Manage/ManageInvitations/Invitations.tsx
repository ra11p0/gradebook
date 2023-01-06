import { faCheck, faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { List, Stack } from '@mui/material';
import moment from 'moment';
import { ReactElement, useState } from 'react';
import { Button, Table } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import InvitationResponse from '../../../../ApiClient/Invitations/Definitions/Responses/InvitationResponse';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import InfiniteScrollWrapper from '../../../Shared/InfiniteScrollWrapper';
import PermissionsBlocker from '../../../Shared/PermissionsBlocker';
import Person from '../../../Shared/Person';
import AddInvitationModal from './AddInvitationModal';

interface InvitationsProps {
  currentSchool: any;
}
const Invitations = (props: InvitationsProps): ReactElement => {
  const [showInvitationModal, setShowInvitationModal] = useState(false);
  const { t } = useTranslation('invitations');
  return (
    <div>
      <Stack>
        <div className="d-flex justify-content-between">
          <h5 className="my-auto">{t('invitations')}</h5>
          <div>
            <PermissionsBlocker
              allowingPermissions={[PermissionLevelEnum.Invitations_CanInvite]}
            >
              <Button
                className="addInvitationButton"
                onClick={() => setShowInvitationModal(true)}
              >
                {t('invitePeople')}
              </Button>
              <AddInvitationModal
                show={showInvitationModal}
                onHide={() => setShowInvitationModal(false)}
              />
            </PermissionsBlocker>
          </div>
        </div>
        <Stack>
          <List>
            <InfiniteScrollWrapper
              wrapper={(items) => (
                <Table striped bordered hover responsive>
                  <thead>
                    <tr>
                      <th>{t('invitationCode')}</th>
                      <th>{t('isUsed')}</th>
                      <th>{t('exprationDate')}</th>
                      <th>{t('person')}</th>
                    </tr>
                  </thead>
                  <tbody>{items}</tbody>
                </Table>
              )}
              mapper={(invitation: InvitationResponse, index) => (
                <tr key={index}>
                  <td className="invitation-code">
                    {invitation.invitationCode}
                  </td>
                  <td>
                    <FontAwesomeIcon
                      icon={invitation.isUsed ? faCheck : faTimes}
                    />
                  </td>
                  <td>
                    {moment.utc(invitation.exprationDate).local().format('lll')}
                  </td>
                  {invitation.invitedPerson && (
                    <td>
                      <Person
                        {...invitation.invitedPerson}
                        guid={invitation.invitedPersonGuid ?? ''}
                      />
                    </td>
                  )}
                </tr>
              )}
              fetch={async (page: number) => {
                if (!props.currentSchool.schoolGuid) return [];
                const response = await SchoolsProxy.getInvitationsInSchool(
                  props.currentSchool.schoolGuid,
                  page
                );
                return response.data as [];
              }}
              effect={[showInvitationModal, props.currentSchool.schoolGuid]}
            />
          </List>
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
)(Invitations);
