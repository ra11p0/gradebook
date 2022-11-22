import { useFormik } from "formik";
import React, { useState } from "react";
import { Accordion, Button, Stack } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import NewCycleStepForm from "./NewCycleStepForm";
import FormikValidationLabel from "../../../../Shared/FormikValidationLabel";
import * as Yup from 'yup';

export type Stage = {
  uuid: string;
  subjects: {
    uuid: string;
    hoursNo: number;
    subjectGuid: string;
  }[];
  name: string;
}

function NewCycleForm() {
  const [openStages, setOpenStages] = useState<string[]>([]);
  const { t } = useTranslation("educationCycle");
  const formik = useFormik({
    initialValues: {
      name: '',
      stages: [] as Stage[]
    },
    validationSchema: Yup.object().shape({
      name: Yup.string().required(t('educationCycleNameIsRequired')).min(3, t('educationCycleNameTooShort')).max(25, t('educationCycleNameTooLong')),
      stages: Yup.array().of(Yup.object().shape({
        name: Yup.string().required(t('educationCycleStepNameIsRequired')).min(3, t('educationCycleNameStepTooShort')).max(25, t('educationCycleStepNameTooLong')),
        subjects: Yup.array().of(Yup.object().shape({
          hoursNo: Yup.number().moreThan(0, t('invalidHoursNumber')).required(),
          subjectGuid: Yup.string().required()
        })).min(1).test('unique', t('subjectSelectedMoreThanOnce'), e => {
          return (new Set(e?.map(o => o.subjectGuid))).size === e?.map(o => o.subjectGuid).length;
        })
      })).min(1)
    }),
    onSubmit: (values) => {
      console.dir(values);
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
        <FormikInput name={'name'} label={t('educationCycleName')} formik={formik} />
        <div>
          <small>{t('educationCycleSteps')}</small>
          <FormikValidationLabel formik={formik} name={`stages`} />
          <Accordion
            activeKey={openStages}
            alwaysOpen
            onSelect={(ei) => { setOpenStages(ei as string[]) }}>
            <DynamicList
              map={(uuid, index) => (
                <div className="m-2 p-2 shadow border rounded-3">
                  <NewCycleStepForm stageUuid={uuid} index={index} formik={formik} />
                </div>)
              }
              list={formik.values.stages.map(e => e.uuid)}
              onAdded={(uuid) => {
                setOpenStages(e => [...e, uuid]);
                formik.setFieldValue('stages', [
                  ...formik.values.stages,
                  {
                    uuid,
                    name: '',
                    subjects: []
                  }
                ])
              }}
              onRemoved={(uuid) => {
                formik.setFieldValue('stages', [
                  ...formik.values.stages.filter((e: any) => e.uuid != uuid)
                ]);
              }}
            />
          </Accordion>
        </div>
        <div className="d-flex justify-content-end m-1 p-1">
          <Button type="submit">{t("addNewCycle")}</Button>
        </div>
      </form>
    </Stack>
  );
}

export default NewCycleForm;
