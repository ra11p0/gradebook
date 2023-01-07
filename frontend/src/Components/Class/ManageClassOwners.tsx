import { Button } from '@mui/material';
import { ReactElement, useState } from 'react';
import { useTranslation } from 'react-i18next';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import TeachersInClassResponse from '../../ApiClient/Classes/Definitions/Responses/TeachersInClassResponse';
import PeopleProxy from '../../ApiClient/People/PeopleProxy';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import Notifications from '../../Notifications/Notifications';
import PeoplePicker from '../Shared/PeoplePicker/PeoplePicker';

interface Props {
  classGuid: string;
  classOwners: TeachersInClassResponse[];
  setClassOwners: (owners: TeachersInClassResponse[]) => void;
  setRefreshKey: (func: (key: any) => void) => void;
}

function ManageClassOwners(props: Props): ReactElement {
  const { t } = useTranslation('classIndex');
  const [showTeachersPicker, setShowTeachersPicker] = useState(false);
  return (
    <>
      <Button
        id="manageClassOwners"
        variant="outlined"
        onClick={() => {
          setShowTeachersPicker((e) => !e);
        }}
      >
        {t('manageClassOwners')}
      </Button>
      <PeoplePicker
        show={showTeachersPicker}
        onHide={() => {
          setShowTeachersPicker(false);
        }}
        onConfirm={(teachers: string[]) => {
          ClassesProxy.addTeachersToClass(props.classGuid ?? '', teachers)
            .then((r) => {
              props.setClassOwners(r.data);
              props.setRefreshKey((e) => e++);
            })
            .catch(Notifications.showApiError);
        }}
        selectedPeople={props.classOwners.map((o) => o.guid)}
        getPeople={async (pickerData, page) => {
          return (
            await PeopleProxy.searchPeople(
              { ...pickerData, schoolRole: SchoolRolesEnum.Teacher },
              page
            )
          ).data;
        }}
      />
    </>
  );
}

export default ManageClassOwners;
