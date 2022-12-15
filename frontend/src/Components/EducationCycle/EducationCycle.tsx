import React, { ReactElement, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
// import { useTranslation } from 'react-i18next';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import EducationCycleResponse from '../../ApiClient/EducationCycles/Definitions/Responses/EducationCycleResponse';
import Notifications from '../../Notifications/Notifications';
import LoadingScreen from '../Shared/LoadingScreen';
import { Stack } from 'react-bootstrap';
import EducationCycleHeader from './EducationCycleHeader';
import EducationCycleAssignedClasses from './EducationCycleAssignedClasses';

interface Props {
  educationCycleGuid?: string;
}

function EducationCycle(props: Props): ReactElement {
  // const { t } = useTranslation('educationCycles');
  const { educationCycleGuid } = useParams();
  const [educationCycle, setEducationCycle] = useState<
    EducationCycleResponse | undefined
  >(undefined);
  useEffect(() => {
    void (async () => {
      const guid = educationCycleGuid ?? props.educationCycleGuid;
      if (!guid) return;
      await EducationCyclesProxy.getEducationCycle(guid)
        .then((res) => setEducationCycle(res.data))
        .catch(Notifications.showApiError);
    })();
  }, [props.educationCycleGuid, educationCycleGuid]);
  return (
    <LoadingScreen isReady={!!educationCycle}>
      <>
        {educationCycle && (
          <>
            <Stack>
              <EducationCycleHeader {...educationCycle} />
              <div>EducationCycle {`${educationCycle?.guid ?? ''}`}</div>
              education cycle assigned classes:
              <div>
                <EducationCycleAssignedClasses
                  educationCycleGuid={educationCycle.guid}
                />
              </div>
            </Stack>
          </>
        )}
      </>
    </LoadingScreen>
  );
}

export default EducationCycle;
