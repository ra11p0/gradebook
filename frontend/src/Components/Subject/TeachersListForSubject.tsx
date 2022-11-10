import React from "react";
import TeachersForSubjectResponse from "../../ApiClient/Subjects/Definitions/Responses/TeachersForSubjectResponse";
import SubjectsProxy from "../../ApiClient/Subjects/SubjectsProxy";
import InfiniteScrollWrapper from "../Shared/InfiniteScrollWrapper";

type Props = {
  subjectGuid: string;
  refreshKey: any;
};

function TeachersListForSubject(props: Props) {
  return (
    <div>
      <InfiniteScrollWrapper
        effect={[props.refreshKey]}
        mapper={(item: TeachersForSubjectResponse, index: number) => <div key={index}>{`${item.name} ${item.surname}`}</div>}
        fetch={async (page: number) => {
          return (await SubjectsProxy.getTeachersForSubject(props.subjectGuid, page)).data;
        }}
      />
    </div>
  );
}

export default TeachersListForSubject;
