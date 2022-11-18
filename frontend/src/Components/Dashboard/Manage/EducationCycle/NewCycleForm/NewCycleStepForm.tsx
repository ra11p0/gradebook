import AsyncSelect from "react-select/async";
import { useFormik } from "formik";
import React, { useEffect, useState } from "react";
import { Accordion, Button, Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import SelectAsyncPaginate from "../../../../Shared/SelectAsyncPaginate";
import SubjectResponse from "../../../../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import { SingleValue } from "react-select";
import { v4 } from "uuid";
import { string } from "yup";

type Props = {
};

function NewCycleStepForm(props: Props) {
  const { t } = useTranslation("educationCycle");
  const [uuid, setUuid] = useState(v4());
  const formik = useFormik({
    initialValues: { name: "" }, onSubmit: (vals) => {
      console.dir(vals)
    }
  });

  const [dispatcher, setDispatcher] = useState<() => string | undefined>(() => (undefined));

  return (
    <Accordion.Item eventKey={uuid}>
      <Accordion.Header>{`${t('step')}: ${formik.values.name}`}</Accordion.Header>
      <Accordion.Body>
        <Row>
          <Col>
            <Row>
              <Col>{t("stage")}</Col>
            </Row>
            <Row>
              <Col>
                <FormikInput name="name" formik={formik} />
                <div>
                  <small>Subjects</small>
                  <DynamicList
                    item={(uuid) => {
                      return (
                        <div className="m-2 p-2 shadow border rounded-3">
                          <DynamicSubjectSelectField onChange={(guid) => {

                          }}
                            dispatcher={(fn) => { setDispatcher(fn); console.dir(fn) }} />
                        </div>)
                    }
                    }
                    onRemoved={(uuid) => {

                    }}
                  />
                </div>
                <Button onClick={() => { console.dir(dispatcher) }}>Sub</Button>
              </Col>
            </Row>
          </Col>
        </Row>
      </Accordion.Body>
    </Accordion.Item>

  );
}

function DynamicSubjectSelectField(props: { onChange: (subjectGuid: string) => void, dispatcher: (dispatch: () => string | undefined) => void }) {
  const [opt, setOpt] = useState<SubjectResponse | undefined>(undefined);
  useEffect(() => {
    props.dispatcher(() => { return opt?.name })
  }, [])
  return (
    <div>

      <SelectAsyncPaginate
        value={opt}
        onChange={(nval: SingleValue<SubjectResponse>, act) => {
          setOpt(nval!);
          props.onChange(nval!.guid);
        }}
        getOptionLabel={(opt) => opt.name}
        getOptionValue={(opt) => opt.guid}
        fetch={async (query: string, page: number) => (await SchoolsProxy.subjects.getSubjectsInSchool(page, query)).data}
      />
    </div>
  );
}

export default NewCycleStepForm;
