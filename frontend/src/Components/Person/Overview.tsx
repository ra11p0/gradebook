import React from "react";
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";
import GroupsList from "./GroupsList";
import ManagedClassesList from "./ManagedClassesList";

type Props = {
  schoolRole: SchoolRolesEnum;
  personGuid: string;
};

function Overview(props: Props) {
  return (
    <div>
      {props.schoolRole === SchoolRolesEnum.Teacher && <ManagedClassesList personGuid={props.personGuid} />}
      <GroupsList />
    </div>
  );
}

export default Overview;
