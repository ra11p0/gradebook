import React, { ReactElement, useEffect, useState } from 'react';
import { Route, Routes, useParams } from 'react-router-dom';
import { ClassResponse } from '../../ApiClient/People/Definitions/Responses/PersonResponse';
import PeopleProxy from '../../ApiClient/People/PeopleProxy';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import LoadingScreen from '../Shared/LoadingScreen';
import Overview from './Overview';
import PersonHeader from './PersonHeader';
import PersonPermissions from './PersonPermissions';

interface Props {
  personGuid?: string;
}

function PersonIndex(props: Props): ReactElement {
  const personGuid = useParams().personGuid ?? props.personGuid;
  const [personName, setPersonName] = useState<string>('');
  const [personSurname, setPersonSurname] = useState<string>('');
  const [personGuidHooked, setPersonGuidHooked] = useState<string>(
    personGuid ?? ''
  );
  const [personSchoolRole, setPersonSchoolRole] = useState<SchoolRolesEnum>(0);
  const [personActiveClass, setPersonActiveClass] = useState<
    ClassResponse | undefined
  >(undefined);
  useEffect(() => {
    if (personGuid) {
      setPersonGuidHooked(personGuid);
      void PeopleProxy.getPerson(personGuid).then((personResponse) => {
        setPersonName(personResponse.data.name);
        setPersonSurname(personResponse.data.surname);
        setPersonSchoolRole(personResponse.data.schoolRole);
        setPersonActiveClass(personResponse.data.activeClass);
      });
    }
  }, [props.personGuid]);
  return (
    <LoadingScreen isReady={!!personName && !!personSurname}>
      <>
        <div className="p-3 bg-light">
          <PersonHeader
            personName={personName}
            personSurname={personSurname}
            personSchoolRole={personSchoolRole}
            activeClass={personActiveClass}
          />
        </div>
        <Routes>
          <Route
            path="permissions"
            element={<PersonPermissions personGuid={personGuidHooked ?? ''} />}
          />
          <Route
            path="/"
            element={
              <Overview
                schoolRole={personSchoolRole}
                personGuid={personGuid ?? ''}
              />
            }
          />
        </Routes>
      </>
    </LoadingScreen>
  );
}

export default PersonIndex;
