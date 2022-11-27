import React, { ReactElement, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import SchoolsProxy from '../../ApiClient/Schools/SchoolsProxy';
import TeachersForSubjectResponse from '../../ApiClient/Subjects/Definitions/Responses/TeachersForSubjectResponse';
import SubjectsProxy from '../../ApiClient/Subjects/SubjectsProxy';
import Notifications from '../../Notifications/Notifications';
import PeoplePicker from '../Shared/PeoplePicker';

interface Props {
  onHide: () => void;
  subjectGuid: string;
}

function AddTeacherToSubjectModal(props: Props): ReactElement {
  const [showPicker, setShowPicker] = useState(false);
  const [selectedTeachers, setSelectedTeachers] = useState<
    TeachersForSubjectResponse[]
  >([]);
  const [refreshKey, setRefreshKey] = useState(0);
  const { t } = useTranslation('subjects');
  useEffect(() => {
    SubjectsProxy.getTeachersForSubject(props.subjectGuid)
      .then((resp) => setSelectedTeachers(resp.data))
      .catch(Notifications.showApiError);
  }, [props.subjectGuid, refreshKey]);
  return (
    <div>
      <Button
        onClick={() => {
          setShowPicker(true);
        }}
      >
        {t('addTeacherToSubject')}
      </Button>
      <PeoplePicker
        selectedPeople={selectedTeachers.map((tea) => tea.guid)}
        onHide={function (): void {
          setShowPicker(false);
          props.onHide();
          setRefreshKey((e) => e + 1);
        }}
        onConfirm={function (peopleGuids: string[]): void {
          SubjectsProxy.editTeachersInSubject(props.subjectGuid, peopleGuids)
            .then(() => {
              setShowPicker(false);
              props.onHide();
              setRefreshKey((e) => e + 1);
            })
            .catch(Notifications.showApiError);
        }}
        getPeople={async (
          schoolGuid: string,
          schoolRole: string,
          query: string,
          page: number
        ) => {
          return (
            await SchoolsProxy.searchPeople(schoolGuid, 'Teacher', query, page)
          ).data;
        }}
        show={showPicker}
      />
    </div>
  );
}

export default AddTeacherToSubjectModal;
