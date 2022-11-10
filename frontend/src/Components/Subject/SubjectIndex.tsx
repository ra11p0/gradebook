import React, { useState } from "react";
import { useParams } from "react-router";
import AddTeacherToSubjectModal from "./AddTeacherToSubjectModal";
import TeachersListForSubject from "./TeachersListForSubject";

type Props = {
  subjectGuid?: string;
};

function SubjectIndex(props: Props) {
  const subjectGuid = useParams().subjectGuid ?? props.subjectGuid ?? "";
  const [refreshKey, setRefreshKey] = useState(0);
  return (
    <div>
      SubjectIndex {subjectGuid}
      <div>
        <AddTeacherToSubjectModal
          onHide={() => {
            setRefreshKey((e) => e + 1);
          }}
          subjectGuid={subjectGuid}
        />
        <TeachersListForSubject subjectGuid={subjectGuid} refreshKey={refreshKey} />
      </div>
    </div>
  );
}

export default SubjectIndex;
