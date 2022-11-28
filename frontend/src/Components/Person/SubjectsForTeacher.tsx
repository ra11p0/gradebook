import React, { ReactElement } from 'react';
import { Card, ListGroup, ListGroupItem } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router';
import SubjectsForTeacherResponse from '../../ApiClient/People/Definitions/Responses/SubjectsForTeacherResponse';
import PeopleProxy from '../../ApiClient/People/PeopleProxy';
import InfiniteScrollWrapper from '../Shared/InfiniteScrollWrapper';

interface Props {
  personGuid: string;
}

function SubjectsForTeacher(props: Props): ReactElement {
  const { t } = useTranslation('person');
  const navigate = useNavigate();
  return (
    <Card>
      <Card.Header>
        <Card.Title>{t('subjectsForTeacher')}</Card.Title>
      </Card.Header>
      <Card.Body>
        <ListGroup>
          <InfiniteScrollWrapper
            mapper={(item: SubjectsForTeacherResponse, index: number) => (
              <ListGroupItem
                key={index}
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/subject/show/${item.guid}`);
                }}
              >
                {item.name}
              </ListGroupItem>
            )}
            fetch={async (page: number) => {
              return (
                await PeopleProxy.subjects.getSubjectsForTeacher(
                  props.personGuid,
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

export default SubjectsForTeacher;
