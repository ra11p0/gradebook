import React, { ReactElement } from 'react';
import { Card, ListGroup, ListGroupItem } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router';
import TeachersForSubjectResponse from '../../ApiClient/Subjects/Definitions/Responses/TeachersForSubjectResponse';
import SubjectsProxy from '../../ApiClient/Subjects/SubjectsProxy';
import InfiniteScrollWrapper from '../Shared/InfiniteScrollWrapper';

interface Props {
  subjectGuid: string;
  refreshKey: any;
}

function TeachersListForSubject(props: Props): ReactElement {
  const { t } = useTranslation('subjects');
  const navigate = useNavigate();
  return (
    <Card>
      <Card.Header>
        <Card.Title>{t('teachersForSubject')}</Card.Title>
      </Card.Header>
      <Card.Body>
        <ListGroup>
          <InfiniteScrollWrapper
            effect={[props.refreshKey]}
            mapper={(item: TeachersForSubjectResponse, index: number) => (
              <ListGroupItem
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/person/show/${item.guid}`);
                }}
                key={index}
              >{`${item.name} ${item.surname}`}</ListGroupItem>
            )}
            fetch={async (page: number) => {
              return (
                await SubjectsProxy.getTeachersForSubject(
                  props.subjectGuid,
                  page
                )
              ).data;
            }}
          />
        </ListGroup>
      </Card.Body>
    </Card>
  );
}

export default TeachersListForSubject;
