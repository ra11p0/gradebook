import React, { ReactElement, useEffect, useState } from 'react';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse from '../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import Notifications from '../../Notifications/Notifications';
import LoadingScreen from '../Shared/LoadingScreen';

interface Props {
  classGuid: string;
}

function EducationCycle({ classGuid }: Props): ReactElement {
  const [educationCycle, setEducationCycle] = useState<
    EducationCyclesInClassResponse | undefined
  >(undefined);
  useEffect(() => {
    void (async () => {
      await ClassesProxy.educationCycles
        .getEducationCyclesInClass(classGuid)
        .then((resp) => {
          setEducationCycle(resp.data);
        })
        .catch(Notifications.showApiError);
    })();
  }, []);

  return (
    <div>
      <LoadingScreen isReady={!!educationCycle}></LoadingScreen>
    </div>
  );
}

export default EducationCycle;
