import React, { ReactElement } from 'react';
import { Stack } from 'react-bootstrap';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../Notifications/Notifications';
import ClassPicker from '../Shared/ClassPicker/ClassPicker';
// import { useTranslation } from 'react-i18next';

interface Props {
  educationCycleGuid: string;
}

function EducationCycleAssignedClasses(props: Props): ReactElement {
  //  const { t } = useTranslation('educationCycle');
  return (
    <Stack>
      <div>
        <div>{props.educationCycleGuid}</div>
        <ClassPicker
          selected={(async () => {
            return (
              await EducationCyclesProxy.getClassesForEducationCycle(
                props.educationCycleGuid
              )
            ).data.map((e) => e.guid);
          })()}
          onClassesSelected={async (guids: string[]) => {
            await EducationCyclesProxy.editClassesInEducationCycle(
              props.educationCycleGuid,
              guids
            ).catch(Notifications.showApiError);
          }}
        />
      </div>
    </Stack>
  );
}

export default EducationCycleAssignedClasses;
