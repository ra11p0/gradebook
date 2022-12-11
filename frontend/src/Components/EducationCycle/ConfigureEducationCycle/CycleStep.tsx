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
      <Stack className="border rounded-2 shadow mx-1 my-2 p-1">
        <small>{`${stage.order + 1}. ${stage.name}`}</small>
        <Row>
          <Col>
            <FormikInput
              type="date"
              label={t('dateSince')}
              name={`stages.${stageKey}.dateSince`}
              formik={formik}
            />
          </Col>
          <Col>
            <FormikInput
              type="date"
              label={t('dateUntil')}
              name={`stages.${stageKey}.dateUntil`}
              formik={formik}
            />
          </Col>
        </Row>

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
