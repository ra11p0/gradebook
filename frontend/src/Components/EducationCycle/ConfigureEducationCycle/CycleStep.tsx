import _ from 'lodash';
import React, { ReactElement } from 'react';
import { Col, Row, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import FormikInput from '../../Shared/FormikInput';
import StepSubject from './StepSubject';

interface Props {
  stageKey: number;
  stage: { name: string; order: number; subjects: any[] };
  formik: any;
}

function CycleStep({ stageKey, stage, formik }: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <>
      <Stack className="border rounded-2 shadow mx-0 my-2 p-1">
        <h6>{`${stage.order + 1}. ${stage.name}`}</h6>
        <Row>
          <Col md={6} s={12}>
            <FormikInput
              type="date"
              label={t('dateSince')}
              name={`stages.${stageKey}.dateSince`}
              formik={formik}
              minDate={
                _.get(formik.values, `stages.${stageKey - 1}.dateUntil`) ??
                _.get(formik.values, 'dateSince')
              }
              maxDate={
                _.get(formik.values, `stages.${stageKey}.dateUntil`) ??
                _.get(formik.values, `stages.${stageKey + 1}.dateSince`) ??
                _.get(formik.values, 'dateUntil')
              }
            />
          </Col>
          <Col md={6} s={12}>
            <FormikInput
              type="date"
              label={t('dateUntil')}
              name={`stages.${stageKey}.dateUntil`}
              formik={formik}
              minDate={
                _.get(formik.values, `stages.${stageKey}.dateSince`) ??
                _.get(formik.values, `stages.${stageKey - 1}.dateUntil`) ??
                _.get(formik.values, 'dateSince')
              }
              maxDate={
                _.get(formik.values, `stages.${stageKey + 1}.dateSince`) ??
                _.get(formik.values, 'dateUntil')
              }
            />
          </Col>
        </Row>
        <h6>{t('subjects')}</h6>
        {stage.subjects.map((subject: any, subjectKey: number) => (
          <StepSubject
            key={subjectKey}
            {...{ subject, stageKey, subjectKey, formik }}
          />
        ))}
      </Stack>
    </>
  );
}

export default CycleStep;
