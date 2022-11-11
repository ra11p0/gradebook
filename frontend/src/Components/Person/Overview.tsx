import React from "react";
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";
import GroupsList from "./GroupsList";
import ManagedClassesList from "./ManagedClassesList";
import SubjectsForTeacher from "./SubjectsForTeacher";

type Props = {
  schoolRole: SchoolRolesEnum;
  personGuid: string;
};

function Overview(props: Props) {
  return (
    <div className="d-flex gap-3 m-2 p-2">
      {props.schoolRole === SchoolRolesEnum.Teacher && (
        <>
          <div>
            <ManagedClassesList personGuid={props.personGuid} />
          </div>
          <div>
            <SubjectsForTeacher personGuid={props.personGuid} />
          </div>
        </>
      )}
      <GroupsList />
    </div>
  );
}

export default Overview;
