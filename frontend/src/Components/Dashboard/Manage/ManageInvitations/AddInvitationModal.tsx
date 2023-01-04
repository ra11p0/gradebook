import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import PeoplePicker from '../../../Shared/PeoplePicker/PeoplePicker';
import PeopleProxy from '../../../../ApiClient/People/PeopleProxy';

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
          },
          props.currentSchool.schoolGuid!
        ).then(() => {
          props.onHide();
        });
      }}
      getPeople={async (pickerData, page) => {
        return (
          await PeopleProxy.searchPeople(
            { ...pickerData, onlyInactive: true },
            page
          )
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
