import moment from 'moment';
import React from 'react';
import { Col, Row, Table } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import SchoolsProxy from '../../../../../ApiClient/Schools/SchoolsProxy';
import InfiniteScrollWrapper from '../../../../Shared/InfiniteScrollWrapper';
import Header from './Header';

function CyclesList(): React.ReactElement {
  const { t } = useTranslation('educationCycle');
  const navigate = useNavigate();
  return (
    <Row>
      <Col>
        <Row>
          <Col>
            <Header />
          </Col>
        </Row>
        <Row>
          <Col>
            <InfiniteScrollWrapper
              wrapper={(items) => (
                <>
                  <Table striped bordered hover responsive>
                    <thead>
                      <tr>
                        <th>{t('educationCycleName')}</th>
                        <th>{t('createdDate')}</th>
                        <th>{t('creator')}</th>
                      </tr>
                    </thead>
                    <tbody>{items}</tbody>
                  </Table>
                </>
              )}
              mapper={(item, index) => (
                <tr
                  key={index}
                  className={'cursor-pointer'}
                  onClick={() => {
                    navigate(`/educationCycle/show/${item.guid}`);
                  }}
                >
                  <td>{item.name}</td>
                  <td>{moment.utc(item.createdDate).local().format('ll')}</td>
                  <td>{`${item.creator.name} ${item.creator.surname}`}</td>
                </tr>
              )}
              fetch={async (page: number) => {
                return (
                  await SchoolsProxy.educationCycles.getEducationCyclesInSchool(
                    page
                  )
                ).data;
              }}
            />
          </Col>
        </Row>
      </Col>
    </Row>
  );
}

export default CyclesList;
