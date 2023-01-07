import { ReactElement } from 'react';
import { connect } from 'react-redux';
import PeopleProxy from '../../../../ApiClient/People/PeopleProxy';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../Notifications/Notifications';
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
        if (peopleGuids.length !== 0)
          await SchoolsProxy.inviteMultiplePeople(
            {
              invitedPersonGuidArray: peopleGuids,
            },
            props.currentSchool.schoolGuid!
          ).then(() => {
            Notifications.showSuccessNotification();
          });
        props.onHide();
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
