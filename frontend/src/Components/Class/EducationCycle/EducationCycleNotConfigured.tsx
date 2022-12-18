import React, { ReactElement } from 'react';
import { Alert } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCyclesInClassResponse from '../../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import ConfigureEducationCycle from '../../EducationCycle/ConfigureEducationCycle/ConfigureEducationCycle';

interface Props {
  classGuid: string;
  educationCycle: EducationCyclesInClassResponse;
  prepareEducationCycle: () => void;
}

function EducationCycleNotConfigured({
  classGuid,
  educationCycle,
  prepareEducationCycle,
}: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <>
      <Alert variant="warning">
        <div className="d-flex justify-content-between">
          <div>{t('educationCycleNotConfigured')}</div>
        </div>
        <div className="d-flex justify-content-end m-1 p-1">
          <ConfigureEducationCycle
            classGuid={classGuid}
            onSubmit={async () => {
              await prepareEducationCycle();
            }}
            educationCycleGuid={educationCycle.activeEducationCycle?.guid}
          />
        </div>
      </Alert>
    </>
  );
}

export default EducationCycleNotConfigured;
