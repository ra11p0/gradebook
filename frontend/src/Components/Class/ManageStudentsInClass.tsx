import { Button } from '@mui/material';
import React, { ReactElement, useState } from 'react';
import { useTranslation } from 'react-i18next';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import StudentInClassResponse from '../../ApiClient/Classes/Definitions/Responses/StudentInClassResponse';
import Notifications from '../../Notifications/Notifications';
import PeoplePicker from '../Shared/PeoplePicker';

interface Props {
  classGuid: string;
  studentsInClass: StudentInClassResponse[];
  setStudentsInClass: (arr: StudentInClassResponse[]) => void;
  setRefreshKey: (func: (key: any) => void) => void;
}

function ManageStudentsInClass(props: Props): ReactElement {
  const { t } = useTranslation('classIndex');

  const [showStudentsPicker, setShowStudentsPicker] = useState(false);
  return (
    <>
      <Button
        variant="outlined"
        onClick={() => {
          setShowStudentsPicker((e) => !e);
        }}
      >
        {t('manageStudentsInClass')}
      </Button>
      <PeoplePicker
        show={showStudentsPicker}
        onHide={() => {
          setShowStudentsPicker(false);
        }}
        onConfirm={(studentsGuids: string[]) => {
          ClassesProxy.addStudentsToClass(props.classGuid ?? '', studentsGuids)
            .then((r) => {
              props.setStudentsInClass(r.data);
              props.setRefreshKey((e) => e++);
            })
            .catch(Notifications.showApiError);
        }}
        selectedPeople={props.studentsInClass.map((s) => s.guid)}
        getPeople={async (
          schoolGuid,
          discriminator: string,
          query: string,
          page: number
        ) => {
          return (
            await ClassesProxy.searchStudentsCandidatesToClassWithCurrent(
              props.classGuid ?? '',
              query,
              page
            )
          ).data;
        }}
      />
    </>
  );
}

export default ManageStudentsInClass;
