import React, { ReactElement } from 'react';
import { useParams } from 'react-router-dom';
import LoadingScreen from '../Shared/LoadingScreen';

function StudentIndex(): ReactElement {
  const { studentGuid } = useParams();
  return (
    <div>
      <LoadingScreen isReady={!!studentGuid}>
        <>{`StudentPage ${studentGuid!}`}</>
      </LoadingScreen>
    </div>
  );
}

export default StudentIndex;
