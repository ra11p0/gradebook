import _ from 'lodash';
import React, { ReactElement } from 'react';
import { Form } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import SubjectsProxy from '../../../ApiClient/Subjects/SubjectsProxy';
import FormikValidationLabel from '../../Shared/FormikValidationLabel';
import SelectAsyncPaginate from '../../Shared/SelectAsyncPaginate';

interface Props {
  key: number;
  subjectKey: number;
  formik: any;
  stageKey: number;
  subject: any;
}

function StepSubject({
  subjectKey,
  formik,
  subject,
  stageKey,
}: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <>
      <div className={'m-1 p-1'}>
        <b>{subject.subjectName}</b>
        <Form.Group>
          <Form.Label>{t('teacher')}</Form.Label>
          <SelectAsyncPaginate
            id={`stages.${stageKey}.subjects.${subjectKey}.teacher`}
            value={(() => {
              const value = _.get(
                formik.values,
                `stages.${stageKey}.subjects.${subjectKey}.teacher`
              );
              if (!value) return;
              return {
                name: value.name,
                surname: value.surname,
                guid: value.guid,
              };
            })()}
            onChange={(value) => {
              formik.setFieldValue(
                `stages.${stageKey}.subjects.${subjectKey}.teacher`,
                value
              );
            }}
            getOptionLabel={(opt) =>
              `${opt.name as string} ${opt.surname as string}`
            }
            getOptionValue={(opt) => opt.guid}
            fetch={async (query: string, page: number) =>
              (
                await SubjectsProxy.getTeachersForSubject(
                  _.get(
                    formik.values,
                    `stages.${stageKey}.subjects.${subjectKey}.subjectGuid`
                  ),
                  page,
                  query
                )
              ).data
            }
          />
          <FormikValidationLabel
            name={`stages.${stageKey}.subjects.${subjectKey}.teacher`}
            formik={formik}
          />
        </Form.Group>
      </div>
      <hr />
    </>
  );
}

export default StepSubject;
