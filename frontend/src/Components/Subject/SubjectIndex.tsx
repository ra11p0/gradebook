import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import SubjectResponse from "../../ApiClient/Subjects/Definitions/Responses/SubjectResponse";
import SubjectsProxy from "../../ApiClient/Subjects/SubjectsProxy";
import Notifications from "../../Notifications/Notifications";
import LoadingScreen from "../Shared/LoadingScreen";
import AddTeacherToSubjectModal from "./AddTeacherToSubjectModal";
import SubjectHeader from "./SubjectHeader";
import TeachersListForSubject from "./TeachersListForSubject";

type Props = {
  subjectGuid?: string;
};

function SubjectIndex(props: Props) {
  const subjectGuid = useParams().subjectGuid ?? props.subjectGuid ?? "";
  const [subject, setSubject] = useState<SubjectResponse | undefined>(undefined);
  const [refreshKey, setRefreshKey] = useState(0);
  useEffect(() => {
    if (subjectGuid)
      SubjectsProxy.getSubject(subjectGuid)
        .then((resp) => {
          setSubject(resp.data);
        })
        .catch(Notifications.showApiError);
  }, []);
  return (
    <LoadingScreen isReady={!!subject}>
      <>
        <div className="p-3 bg-light">
          <SubjectHeader
            subjectName={subject?.name ?? ""}
            subjectGuid={subjectGuid}
            setRefreshKey={() => {
              setRefreshKey((e) => e + 1);
            }}
          />
        </div>
        <div className="d-flex gap-3 m-2 p-2">
          <TeachersListForSubject subjectGuid={subjectGuid} refreshKey={refreshKey} />
        </div>
      </>
    </LoadingScreen>
  );
}

export default SubjectIndex;
