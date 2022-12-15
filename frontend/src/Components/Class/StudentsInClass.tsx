import React, { ReactElement } from 'react';
import { Card, Table } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import StudentInClassResponse from '../../ApiClient/Classes/Definitions/Responses/StudentInClassResponse';

interface Props {
  studentsInClass: StudentInClassResponse[];
}

function StudentsInClass(props: Props): ReactElement {
  const { t } = useTranslation('classIndex');
  const navigate = useNavigate();
  return (
    <Card>
      <Card.Header>
        <Card.Title>{t('studentsInClass')}</Card.Title>
      </Card.Header>
      <Card.Body className="max-vh-50 overflow-auto">
        <Table striped bordered hover responsive>
          <thead>
            <tr>
              <td>{t('index')}</td>
              <td>{t('name')}</td>
              <td>{t('surname')}</td>
            </tr>
          </thead>
          <tbody>
            {props.studentsInClass.map((student, index) => (
              <tr
                onClick={() => {
                  navigate(`/person/show/${student.guid}`);
                }}
                className={'cursor-pointer'}
                key={index}
              >
                <td>{index + 1}.</td>
                <td>{student.name}</td>
                <td>{student.surname}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Card.Body>
    </Card>
  );
}

export default StudentsInClass;
