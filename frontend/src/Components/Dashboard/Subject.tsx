import React from "react";
import SubjectResponse from "../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import InfiniteScrollWrapper from "../Shared/InfiniteScrollWrapper";
import NewSubjectModalWithButton from "./Manage/ManageSubjects/NewSubjectModalWithButton";

type Props = {};

function Subject({}: Props) {
  return (
    <div>
      <NewSubjectModalWithButton />{" "}
      <div>
        <InfiniteScrollWrapper
          mapper={(item: SubjectResponse, index: number) => <div>{item.name}</div>}
          fetch={async (page: number) => {
            return (await SchoolsProxy.subjects.getSubjectsInSchool(page)).data;
          }}
        />
      </div>
    </div>
  );
}

export default Subject;
