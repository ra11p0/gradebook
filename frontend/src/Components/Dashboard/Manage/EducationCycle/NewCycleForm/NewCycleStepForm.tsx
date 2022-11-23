import React, { useState } from "react";
import { Accordion, Button, Col, Row, Stack } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import SelectAsyncPaginate from "../../../../Shared/SelectAsyncPaginate";
import SubjectResponse from "../../../../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import { SingleValue } from "react-select";
import FormikValidationLabel from "../../../../Shared/FormikValidationLabel";
import _ from "lodash";
import { Stage } from "./NewCycleForm";



type Formik = {
  setFieldValue: (fieldName: string, value: any) => void;
  handleChange: (evt: React.ChangeEvent<any>) => void;
  handleBlur: (evt: React.ChangeEvent<any>) => void;
  errors: any,
  touched: any
  values: {
    stages: Stage[]
  },
}

type Props = {
  stageUuid: string;
  formik: Formik;
  index: number;
};

function NewCycleStepForm(props: Props) {
  const { t } = useTranslation("educationCycle");

  return (
    <Accordion.Item eventKey={props.stageUuid} className={`${(_.get(props.formik.errors.stages, props.index) && _.get(props.formik.touched.stages, props.index)) ? 'border border-danger' : ''}`} >
      <Accordion.Header >{`${props.index + 1}. ${_.isEmpty(props.formik.values.stages[props.index]?.name) ? t('educationCycleStep') : props.formik.values.stages[props.index]?.name}`}</Accordion.Header>
      <Accordion.Body>
        <Row>
          <Col>
            <Row>
              <Col>
                <FormikInput name={`stages.${props.index}.name`} label={t('stepName')} formik={props.formik} />
                <div>
                  <small>{t('stepSubjects')}</small>
                  <FormikValidationLabel name={`stages.${props.index}.subjects`} formik={props.formik} />
                  <DynamicList
                    map={
                      (uuid, key) => (
                        <div className="m-2 p-2 shadow border rounded-3">
                          <DynamicSubjectSelectField uuid={uuid} index={key} parentIndex={props.index} formik={props.formik} />
                        </div>)

                    }
                    list={props.formik.values.stages[props.index]?.subjects?.map(e => e.uuid)}
                    onAdded={(uuid) => {
                      props.formik.setFieldValue(`stages.${props.index}.subjects`, [
                        ...props.formik.values?.stages[props.index]?.subjects.concat(),
                        {
                          uuid,
                          hoursNo: '',
                          subjectGuid: '',
                          isMandatory: true,
                          canUseGroups: false
                        }
                      ])
                    }}
                    onRemoved={(uuid, index) => {
                      props.formik.setFieldValue(`stages.${props.index}.subjects`, _.remove(_.get(props.formik.values.stages, props.index).subjects, (el, ind) => ind != index));
                    }}
                  />
                </div>
              </Col>
            </Row>
          </Col>
        </Row>
      </Accordion.Body >
    </Accordion.Item >

  );
}

type DynamicSubjectSelectFieldProps = {
  index: number;
  parentIndex: number;
  formik: Formik;
  uuid: string;
}

function DynamicSubjectSelectField(props: DynamicSubjectSelectFieldProps) {
  const [opt, setOpt] = useState<SubjectResponse | undefined>(undefined);
  const { t } = useTranslation('educationCycle');

  return (
    <Stack>
      <SelectAsyncPaginate
        value={opt}
        onChange={(nval: SingleValue<SubjectResponse>, act) => {
          setOpt(nval!);
          props.formik.setFieldValue(`stages.${props.parentIndex}.subjects.${props.index}.subjectGuid`, nval?.guid)
        }}
        getOptionLabel={(opt) => opt.name}
        getOptionValue={(opt) => opt.guid}
        fetch={async (query: string, page: number) => (await SchoolsProxy.subjects.getSubjectsInSchool(page, query)).data}
      />
      <FormikValidationLabel name={`stages.${props.parentIndex}.subjects.${props.index}.subjectGuid`} formik={props.formik} />
      <FormikInput name={`stages.${props.parentIndex}.subjects.${props.index}.hoursNo`} label={t('hoursNo')} formik={props.formik} />
      <FormikInput name={`stages.${props.parentIndex}.subjects.${props.index}.isMandatory`} label={t('isMandatory')} type='switch' formik={props.formik} />
      <FormikInput name={`stages.${props.parentIndex}.subjects.${props.index}.canUseGroups`} label={t('canUseGroups')} type='switch' formik={props.formik} />
    </Stack>
  );
}

export default NewCycleStepForm;
