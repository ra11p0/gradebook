import { useFormik } from 'formik';
import React, { ReactElement, useState } from 'react';
import { Accordion, Button, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import DynamicList from '../../../../Shared/DynamicList';
import FormikInput from '../../../../Shared/FormikInput';
import NewCycleStepForm from './NewCycleStepForm';
import FormikValidationLabel from '../../../../Shared/FormikValidationLabel';
import * as Yup from 'yup';
import _ from 'lodash';
import SchoolsProxy from '../../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../../Notifications/Notifications';

export interface Stage {
  uuid: string;
  guid?: string;
  subjects: Array<{
    guid?: string;
    uuid: string;
    hoursNo: number;
    subjectGuid: string;
    isMandatory: boolean;
    canUseGroups: boolean;
  }>;
  name: string;
}

function NewCycleForm(): ReactElement {
  const [openStages, setOpenStages] = useState<string[]>([]);
  const { t } = useTranslation('educationCycle');
  const formik = useFormik({
    initialValues: {
      name: '',
      stages: [] as Stage[],
    },
    validationSchema: Yup.object().shape({
      name: Yup.string()
        .required(t('educationCycleNameIsRequired'))
        .min(3, t('educationCycleNameTooShort'))
        .max(25, t('educationCycleNameTooLong')),
      stages: Yup.array()
        .of(
          Yup.object().shape({
            name: Yup.string()
              .required(t('educationCycleStepNameIsRequired'))
              .min(3, t('educationCycleNameStepTooShort'))
              .max(25, t('educationCycleStepNameTooLong')),
            subjects: Yup.array()
              .of(
                Yup.object().shape({
                  hoursNo: Yup.number()
                    .typeError(t('fieldNumberOnly'))
                    .moreThan(0, t('invalidHoursNumber'))
                    .required(t('fieldRequired')),
                  subjectGuid: Yup.string().required(t('fieldRequired')),
                })
              )
              .min(1, t('atLeastOneSubject'))
              .test('unique', t('subjectSelectedMoreThanOnce'), (e) => {
                return (
                  new Set(e?.map((o) => o.subjectGuid)).size ===
                  e?.map((o) => o.subjectGuid).length
                );
              }),
          })
        )
        .min(1, t('atLeastOneStep')),
    }),
    onSubmit: async (values) => {
      await SchoolsProxy.educationCycles
        .addEducationCycle(values)
        .then((e) => Notifications.showSuccessNotification('success', e.data));
    },
  });
  return (
    <Stack>
      <div className="d-flex justify-content-between">
        <div>
          <h5>{t('addNewCycleForm')}</h5>
        </div>
      </div>
      <form onSubmit={formik.handleSubmit}>
        <FormikInput
          name={'name'}
          label={t('educationCycleName')}
          formik={formik}
        />
        <div>
          <small>{t('educationCycleSteps')}</small>
          <FormikValidationLabel formik={formik} name={`stages`} />
          <Accordion
            activeKey={openStages}
            alwaysOpen
            onSelect={(ei) => {
              setOpenStages(ei as string[]);
            }}
          >
            <DynamicList
              map={(uuid, index) => (
                <div className="m-2 p-2 shadow border rounded-3">
                  <NewCycleStepForm
                    stageUuid={uuid}
                    index={index}
                    formik={formik}
                  />
                </div>
              )}
              list={formik.values.stages.map((e) => e.uuid)}
              onAdded={async (uuid) => {
                setOpenStages((e) => [...e, uuid]);
                await formik.setFieldValue('stages', [
                  ...formik.values.stages,
                  {
                    uuid,
                    name: '',
                    subjects: [],
                  },
                ]);
              }}
              onRemoved={async (uuid, index) => {
                await formik.setFieldValue(
                  'stages',
                  _.remove(formik.values.stages, (el, ind) => ind !== index)
                );
              }}
            />
          </Accordion>
        </div>
        <div className="d-flex justify-content-end m-1 p-1">
          <Button type="submit">{t('addNewCycle')}</Button>
        </div>
      </form>
    </Stack>
  );
}

export default NewCycleForm;
