import React, { ReactElement } from 'react';
import { Stack } from 'react-bootstrap';
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
          onClassesSelected={(guids: string[]) => {
            console.dir(guids);
          }}
        />
      </div>
    </Stack>
  );
}

export default EducationCycleAssignedClasses;
