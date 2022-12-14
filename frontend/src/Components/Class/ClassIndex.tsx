import React, { ReactElement, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import ClassResponse from '../../ApiClient/Schools/Definitions/Responses/ClassResponse';
import Notifications from '../../Notifications/Notifications';
import LoadingScreen from '../Shared/LoadingScreen';
import ApplicationModes from '@ra11p0/forms-app/dist/Constraints/ApplicationModes';
import Field from '@ra11p0/forms-app/dist/Interfaces/Common/Field';
import FormFilledResult from '@ra11p0/forms-app/dist/Interfaces/Common/FormFilledResult';
import Forms from '@ra11p0/forms-app/dist/Components/Forms';
import TeachersInClassResponse from '../../ApiClient/Classes/Definitions/Responses/TeachersInClassResponse';
import { connect } from 'react-redux';
import getApplicationLanguageReduxProxy from '../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import StudentInClassResponse from '../../ApiClient/Classes/Definitions/Responses/StudentInClassResponse';
import StudentsInClass from './StudentsInClass';
import ClassHeader from './ClassHeader';
import { GlobalState } from '../../store';
import EducationCycle from './EducationCycle/EducationCycle';

interface Props {
  localization: string;
}

function ClassIndex(props: Props): ReactElement {
  const { classGuid } = useParams();
  const [_class, setClass] = useState<ClassResponse | null>(null);
  const [studentsInClass, setStudentsInClass] = useState<
    StudentInClassResponse[]
  >([]);
  const [classOwners, setClassOwners] = useState<TeachersInClassResponse[]>([]);
  const [isReady, setIsReady] = useState(false);
  const [refreshKey, setRefreshKey] = useState<any>(0);
  useEffect(() => {
    if (!classGuid) return;
    void Promise.all([
      ClassesProxy.getClass(classGuid)
        .then((r) => setClass(r.data))
        .catch(Notifications.showApiError),
      ClassesProxy.getStudentsInClass(classGuid)
        .then((r) => setStudentsInClass(r.data))
        .catch(Notifications.showApiError),
      ClassesProxy.getTeachersInClass(classGuid)
        .then((r) => setClassOwners(r.data))
        .catch(Notifications.showApiError),
    ]).then(() => setIsReady(true));
  }, [classGuid, refreshKey]);
  return (
    <LoadingScreen isReady={isReady}>
      <>
        <div className="bg-light p-3">
          <ClassHeader
            class={_class ?? ({} as ClassResponse)}
            classGuid={classGuid ?? ''}
            classOwners={classOwners}
            studentsInClass={studentsInClass}
            setRefreshKey={setRefreshKey}
            setClassOwners={setClassOwners}
            setStudentsInClass={setStudentsInClass}
          />
        </div>
        <div className="d-flex flex-wrap">
          <div className="m-4">
            <StudentsInClass studentsInClass={studentsInClass} />
          </div>
          <div className="m-4">
            <EducationCycle classGuid={classGuid!} />
          </div>
        </div>

        <div className="m-4">
          <Forms
            localization={props.localization}
            mode={ApplicationModes.Edit}
            onSubmit={(result: Field[] | FormFilledResult): void => {}}
            onDiscard={() => {}}
          />
        </div>
      </>
    </LoadingScreen>
  );
}

export default connect(
  (state: GlobalState) => ({
    localization: getApplicationLanguageReduxProxy(state),
  }),
  () => ({})
)(ClassIndex);
