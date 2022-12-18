import React, { ReactElement } from 'react';
import { Stack } from 'react-bootstrap';
import EducationCycleStepInstanceSmall from './EducationCycleStepInstanceSmall';
import { EducationCycleStepInstance } from '../../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import EducationCycleStepInstanceCurrent from './EducationCycleStepInstanceCurrent';

interface Props {
  getCurrentAndSurroundingStepInstances: () =>
    | undefined
    | {
        current?: EducationCycleStepInstance;
        previous?: EducationCycleStepInstance;
        next?: EducationCycleStepInstance;
      };
  prepareEducationCycle: () => void;
  classGuid: string;
}

function EducationCycleStepInstances({
  getCurrentAndSurroundingStepInstances,
  prepareEducationCycle,
  classGuid,
}: Props): ReactElement {
  return (
    <Stack>
      {getCurrentAndSurroundingStepInstances()?.previous && (
        <EducationCycleStepInstanceSmall
          {...getCurrentAndSurroundingStepInstances()!.previous!}
        />
      )}
      {getCurrentAndSurroundingStepInstances()?.current && (
        <EducationCycleStepInstanceCurrent
          {...getCurrentAndSurroundingStepInstances()!.current!}
          isLast={!getCurrentAndSurroundingStepInstances()?.next}
          stateChanged={async () => {
            await prepareEducationCycle();
          }}
          classGuid={classGuid}
        />
      )}
      {getCurrentAndSurroundingStepInstances()?.next && (
        <EducationCycleStepInstanceSmall
          {...getCurrentAndSurroundingStepInstances()!.next!}
        />
      )}
    </Stack>
  );
}

export default EducationCycleStepInstances;
