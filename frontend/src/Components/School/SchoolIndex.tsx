import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import GetSchoolResponse from "../../ApiClient/Schools/Definitions/GetSchoolResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../Notifications/Notifications";

type Props = {};

function SchoolIndex(props: Props) {
  const { schoolGuid } = useParams();
  const [school, setSchool] = useState<GetSchoolResponse | null>(null);
  useEffect(() => {
    SchoolsProxy.getSchool(schoolGuid!)
      .then((schoolResponse) => {
        setSchool(schoolResponse.data);
      })
      .catch((err) => {
        Notifications.showApiError(err);
      });
  }, []);
  return <div>{`SchoolPage ${JSON.stringify(school)}`}</div>;
}

export default SchoolIndex;
