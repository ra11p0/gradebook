import React, { ReactElement } from 'react';
import { Alert } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCyclesProxy from '../../../ApiClient/EducationCycles/EducationCyclesProxy';
import EducationCyclePicker from '../../Shared/EducationCyclePicker/EducationCyclePicker';

interface Props {
  classGuid: string;
  prepareEducationCycle: () => void;
}

function EducationCycleNotAttached({
  classGuid,
  prepareEducationCycle,
}: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <Alert variant="warning">
      <div className="d-flex justify-content-between">
        <div>{t('educationCycleNotAttached')}</div>
      </div>
      <div className="d-flex justify-content-end m-1 p-1">
        <EducationCyclePicker
          onCyclesSelected={async (selected) => {
            const selectedOne = selected.find(() => true);
            if (!selectedOne) return;
            await EducationCyclesProxy.setEducationCycleForClass(
              selectedOne,
              classGuid
            );
            prepareEducationCycle();
          }}
          onlyOne={true}
        />
      </div>
    </Alert>
  );
}

export default EducationCycleNotAttached;
