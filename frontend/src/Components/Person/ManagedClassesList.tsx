import React from "react";
import { Card, ListGroup, ListGroupItem } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router";
import { ClassResponse } from "../../ApiClient/People/Definitions/PersonResponse";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import InfiniteScrollWrapper from "../Shared/InfiniteScrollWrapper";

type Props = {
  personGuid: string;
};

function ManagedClassesList(props: Props) {
  const { t } = useTranslation("person");
  const navigate = useNavigate();
  return (
    <Card>
      <Card.Header>
        <Card.Title>{t("managedClasses")}</Card.Title>
      </Card.Header>
      <Card.Body>
        <ListGroup>
          <InfiniteScrollWrapper
            mapper={(item: ClassResponse, index: number) => (
              <ListGroupItem
                key={index}
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/class/show/${item.guid}`);
                }}
              >
                {item.name}
              </ListGroupItem>
            )}
            fetch={async (page: number) => {
              return (await PeopleProxy.getClassesForPerson(props.personGuid, page)).data;
            }}
          />
        </ListGroup>
      </Card.Body>
    </Card>
  );
}

export default ManagedClassesList;
