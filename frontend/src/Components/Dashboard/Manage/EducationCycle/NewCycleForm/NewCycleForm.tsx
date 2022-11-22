import { useFormik } from "formik";
import React, { useState } from "react";
import { Accordion, AccordionCollapse, Button, Stack } from "react-bootstrap";
import AccordionItem from "react-bootstrap/esm/AccordionItem";
import addStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/addStageRedux'
import removeStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/removeStageRedux'
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import NewCycleStepForm from "./NewCycleStepForm";
import getStagesRedux from "../../../../../Redux/ReduxQueries/newEducationCycleForm/getStagesRedux";
import getOpenStagesRedux from '../../../../../Redux/ReduxQueries/newEducationCycleForm/getOpenStagesRedux'
import { connect } from "react-redux";
import setOpenStagesRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/setOpenStagesRedux';
import addOpenStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/addOpenStageRedux';
import FormikValidationLabel from "../../../../Shared/FormikValidationLabel";

type Props = {
  stages: string[],
  openStages: string[]
};

function NewCycleForm(props: Props) {
  const { t } = useTranslation("educationCycle");
  const [validationKey, setValidationKey] = useState(0);
  const [hasError, setHasError] = useState(false);
  const formik = useFormik({
    initialValues: { name: '' },
    validate: (vals) => {
      const errors: any = {};

      if (vals.name.length < 3) errors.name = t('invalidName')
      const stages = getStagesRedux();
      if (stages.length < 1) errors.stages = t('invalidStagesCount')
      return errors;
    },
    onSubmit: (values) => {
      setHasError(false);
      setValidationKey(e => e + 1);
      if (!hasError) console.dir(values);
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

          <Accordion
            activeKey={props.openStages}
            alwaysOpen
            onSelect={(ei) => { setOpenStagesRedux(ei as string[]) }}>
            <DynamicList
              map={(uuid: string) => {
                return (
                  <div className="m-2 p-2 shadow border rounded-3">
                    <NewCycleStepForm stageUuid={uuid} validationKey={validationKey} onValidation={(isValid) => { if (!isValid) setHasError(!isValid) }} />
                  </div>
                )
              }}
              list={props.stages}
              onAdded={(uuid) => {
                formik.setFieldError('stages', undefined)
                addStageRedux({ uuid });
                addOpenStageRedux(uuid);
              }}
              onRemoved={(uuid) => {
                removeStageRedux(uuid);
              }}
            />

          </Accordion>
          <FormikValidationLabel formik={formik} name='stages' showDespiteTouching={true} />
        </div>
        <div className="d-flex justify-content-end m-1 p-1">
          <Button type="submit">{t("addNewCycle")}</Button>
        </div>
      </form>
    </Stack>
  );
}

export default connect(
  (state: any) =>
  (
    {
      stages: getStagesRedux(state).map(st => st.uuid),
      openStages: getOpenStagesRedux(state)
    }
  )
  ,
  () =>
  (
    {}
  )
)(NewCycleForm);
