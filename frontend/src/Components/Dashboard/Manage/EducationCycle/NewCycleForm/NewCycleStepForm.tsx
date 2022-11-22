import AsyncSelect from "react-select/async";
import { useFormik } from "formik";
import React, { useEffect, useState } from "react";
import { Accordion, Button, Col, Row, Stack } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import SelectAsyncPaginate from "../../../../Shared/SelectAsyncPaginate";
import SubjectResponse from "../../../../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import { SingleValue } from "react-select";
import { v4 } from "uuid";
import { string } from "yup";
import addSubjectToStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/addSubjectToStageRedux';
import removeSubjectFromStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/removeSubjectFromStageRedux';
import setSubjectGuidForSubjectInStageRedux from '../../../../../Redux/ReduxCommands/newEducationCycleForm/setSubjectGuidForSubjectInStageRedux';
import { connect } from "react-redux";
import getStagesRedux from "../../../../../Redux/ReduxQueries/newEducationCycleForm/getStagesRedux";
import FormikValidationLabel from "../../../../Shared/FormikValidationLabel";
import _ from "lodash";

type Props = {
  validationKey: any,
  onValidation: (isValid: boolean) => void,
  stageUuid: string,
  stages: {
    uuid: string;
    subjects: {
      uuid: string;
    }[]
  }[]
};

function NewCycleStepForm(props: Props) {
  const { t } = useTranslation("educationCycle");
  const [hasError, setHasError] = useState(false);
  const formik = useFormik({
    initialValues: { name: "" },
    validate: (vals) => {
      const errors: any = {};

      if (vals.name.length < 3) errors.name = t('invalidName')
      const subjectsForStage = getStagesRedux().find(e => e.uuid == props.stageUuid)?.subjects;
      if (subjectsForStage?.length == 0) errors.subjects = t('invalidSubjectsCount')
      return errors;
    },
    onSubmit: (vals) => {

    }
  });


  useEffect(() => {
    if (props.validationKey < 1) return;
    (async () => {
      setHasError(false);
      const errors = await formik.validateForm();
      if (!(_.isEmpty(errors))) setHasError(true);
      formik.setFieldTouched('subjects');
      formik.setFieldTouched('name');
      props.onValidation(!hasError);
    })()
  }, [props.validationKey]);


  return (
    <Accordion.Item eventKey={props.stageUuid} className={`${hasError ? `border border-danger ` : ``}`}>
      <Accordion.Header >{`${t('educationCycleStep')}: ${formik.values.name}`}</Accordion.Header>
      <Accordion.Body>
        <Row>
          <Col>
            <Row>
              <Col>
                <FormikInput name="name" label={t('stepName')} formik={formik} />
                <div>
                  <small>{t('stepSubjects')}</small>
                  <DynamicList
                    map={
                      (uuid) => {
                        return (
                          <div className="m-2 p-2 shadow border rounded-3">
                            <DynamicSubjectSelectField
                              validationKey={props.validationKey}
                              onValidation={(isValid) => {
                                if (!isValid) setHasError(true);
                                formik.setFieldTouched('subjects')
                              }}
                              onChange={(guid) => {
                                setSubjectGuidForSubjectInStageRedux(guid, uuid);
                              }} />
                          </div>)
                      }
                    }
                    list={props.stages.find(e => e.uuid == props.stageUuid)!.subjects.map(e => e.uuid)}
                    onRemoved={(uuid) => {
                      removeSubjectFromStageRedux(uuid)
                    }}
                    onAdded={(uuid) => {
                      addSubjectToStageRedux({ uuid }, props.stageUuid)
                    }}
                  />
                  <FormikValidationLabel name="subjects" formik={formik} />
                </div>
              </Col>
            </Row>
          </Col>
        </Row>
      </Accordion.Body >
    </Accordion.Item >

  );
}

function DynamicSubjectSelectField(props: { onChange: (subjectGuid: string) => void, validationKey: any, onValidation: (isValid: boolean) => void }) {
  const [opt, setOpt] = useState<SubjectResponse | undefined>(undefined);
  const { t } = useTranslation('educationCycle');
  const formik = useFormik({
    initialValues: { hoursNo: '', subject: '' },
    validate: (vals) => {
      console.log('inValidation')
      const errors: any = {};
      if (!vals.hoursNo || parseInt(vals.hoursNo) <= 0) errors.hoursNo = t('invalidHoursNumber')
      if (!opt) errors.subject = t('invalidSubject')
      console.dir(errors)
      return errors;
    },
    onSubmit: () => { }
  })

  useEffect(() => {
    if (props.validationKey < 1) return;
    formik.validateForm()
      .then(errors => {
        formik.setFieldTouched('subject');
        formik.setFieldTouched('hoursNo');
        props.onValidation(_.isEmpty(errors));
      });
  }, [props.validationKey])

  return (
    <Stack>
      <SelectAsyncPaginate
        value={opt}
        onChange={(nval: SingleValue<SubjectResponse>, act) => {
          setOpt(nval!);
          props.onChange(nval!.guid);
        }}
        onMenuClose={() => { formik.setFieldTouched('subject') }}
        getOptionLabel={(opt) => opt.name}
        getOptionValue={(opt) => opt.guid}
        fetch={async (query: string, page: number) => (await SchoolsProxy.subjects.getSubjectsInSchool(page, query)).data}
      />
      <FormikValidationLabel name="subject" formik={formik} />
      <FormikInput name="hoursNo" label={t('hoursNo')} formik={formik} />
    </Stack>
  );
}

export default connect((state: any) => ({
  stages: getStagesRedux(state)
}), () => ({}))(NewCycleStepForm);
