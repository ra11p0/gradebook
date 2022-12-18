import React, { ReactElement } from 'react';
import { Col, ListGroup, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import getCurrentUserIdReduxProxy from '../../../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';
import DefaultSchoolSetting from './Settings/DefaultSchoolSetting';

interface Props {
  currentUserGuid?: string;
  formik: any;
}

function AccountSettingsIndex(props: Props): ReactElement {
  const { t } = useTranslation('settings');

  return (
    <>
      <ListGroup>
        <ListGroup.Item>
          <Row>
            <Col className="my-auto">
              <div className="">{t('defaultSchool')}</div>
            </Col>
            <Col>{t('defaultSchoolDescription')}</Col>
            <Col>
              <DefaultSchoolSetting formik={props.formik} />
            </Col>
          </Row>
        </ListGroup.Item>
      </ListGroup>
    </>
  );
}

export default connect(
  (state) => ({
    currentUserGuid: getCurrentUserIdReduxProxy(state),
  }),
  () => ({})
)(AccountSettingsIndex);
