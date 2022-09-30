import React, { useEffect, useState } from "react";
import { Route, Routes, useParams } from "react-router-dom";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import Overview from "./Overview";
import PersonNavigation from "./PersonNavigation";
import PersonPermissions from "./PersonPermissions";

type Props = {
  personGuid?: string;
};

function PersonIndex(props: Props) {
  const personGuid = useParams().personGuid ?? props.personGuid;
  const [personName, setPersonName] = useState<string>("");
  const [personSurname, setPersonSurname] = useState<string>("");
  const [personGuidHooked, setPersonGuidHooked] = useState<string>(personGuid ?? "");
  useEffect(() => {
    if (personGuid) {
      setPersonGuidHooked(personGuid);
      PeopleProxy.getPerson(personGuid).then((personResponse) => {
        setPersonName(personResponse.data.name);
        setPersonSurname(personResponse.data.surname);
      });
    }
  }, [props.personGuid]);
  return (
    <div>
      <div className="p-3 bg-light">
        <PersonNavigation personName={personName} personSurname={personSurname} />
      </div>
      <Routes>
        <Route path="permissions" element={<PersonPermissions personGuid={personGuidHooked ?? ""} />} />
        <Route path="/" element={<Overview />} />
      </Routes>
    </div>
  );
}

export default PersonIndex;
