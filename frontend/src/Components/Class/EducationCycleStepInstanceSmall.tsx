import moment from 'moment';
import React, { ReactElement } from 'react';
import { EducationCycleStepInstance } from '../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';

function EducationCycleStepInstanceSmall(
  props: EducationCycleStepInstance
): ReactElement {
  return (
    <>
      <small className="text-secondary">{`${props.educationCycleStepName}${
        props.dateSince && props.dateUntil
          ? ` (${moment.utc(props.dateSince).local().format('ll')}
           - 
          ${moment.utc(props.dateUntil).local().format('ll')})`
          : ''
      }`}</small>
    </>
  );
}

export default EducationCycleStepInstanceSmall;
