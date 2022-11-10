import React, { useState } from "react";
import { ListGroup, ListGroupItem } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import SubjectResponse from "../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import InfiniteScrollWrapper from "../Shared/InfiniteScrollWrapper";
import NewSubjectModalWithButton from "./Manage/ManageSubjects/NewSubjectModalWithButton";

type Props = {};

function Subject({}: Props) {
  const [refreshKey, setRefreshKey] = useState(0);
  const navigate = useNavigate();
  return (
    <div>
      <NewSubjectModalWithButton
        onHide={() => {
          setRefreshKey((e) => e + 1);
        }}
      />
      <ListGroup>
        <InfiniteScrollWrapper
          mapper={(item: SubjectResponse, index: number) => (
            <ListGroupItem
              className="cursor-pointer"
              key={index}
              onClick={() => {
                navigate(`/subject/show/${item.guid}`);
              }}
            >
              {item.name}
            </ListGroupItem>
          )}
          fetch={async (page: number) => {
            return (await SchoolsProxy.subjects.getSubjectsInSchool(page)).data;
          }}
          effect={[refreshKey]}
        />
      </ListGroup>
    </div>
  );
}

export default Subject;
