import AsyncSelect from "react-select/async";
import { useFormik } from "formik";
import React, { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import SelectAsyncPaginate from "../../../../Shared/SelectAsyncPaginate";
import SubjectResponse from "../../../../../ApiClient/Schools/Definitions/Responses/SubjectResponse";
import { SingleValue } from "react-select";

type Props = {};

function NewCycleStepForm(props: Props) {
  const { t } = useTranslation("educationCycle");
  const formik = useFormik({ initialValues: { name: "" }, onSubmit: (vals) => {} });

  return (
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
                item={
                  <div className="m-2 p-2 shadow border rounded-3">
                    <DynamicSubjectSelectField />
                  </div>
                }
              />
            </div>
          </Col>
        </Row>
      </Col>
    </Row>
  );
}

function DynamicSubjectSelectField() {
  const [opt, setOpt] = useState<SubjectResponse | undefined>(undefined);
  return (
    <div>
      <SelectAsyncPaginate
        value={opt}
        onChange={(nval: SingleValue<SubjectResponse>, act) => {
          setOpt(nval!);
        }}
        getOptionLabel={(opt) => opt.name}
        getOptionValue={(opt) => opt.guid}
        fetch={async (query: string, page: number) => (await SchoolsProxy.subjects.getSubjectsInSchool(page, query)).data}
      />
    </div>
  );
}

export default NewCycleStepForm;
