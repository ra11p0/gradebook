import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import SchoolRolesEnum from '../../../../Common/Enums/SchoolRolesEnum';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import PeoplePicker from '../../../Shared/PeoplePicker/PeoplePicker';

interface AddInvitationModalProps {
  show: boolean;
  onHide: () => void;
  currentSchool: any;
}
const AddInvitationModal = (props: AddInvitationModalProps): ReactElement => {
  return (
    <PeoplePicker
      showFilters
      show={props.show}
      onHide={props.onHide}
      onConfirm={async (peopleGuids: string[]) => {
        await SchoolsProxy.inviteMultiplePeople(
          {
            invitedPersonGuidArray: peopleGuids,
            role: SchoolRolesEnum.Student,
          },
          props.currentSchool.schoolGuid!
        ).then(() => {
          props.onHide();
        });
      }}
      getPeople={async (
        schoolGuid: string,
        schoolRole: string,
        query: string,
        page: number
      ) => {
        return (
          await SchoolsProxy.getInactiveAccessibleStudentsInSchool(schoolGuid)
        ).data;
      }}
    />
  );
};
export default connect(
  (state) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
  }),
  () => ({})
)(AddInvitationModal);
