import { Formik, FormikHelpers, Field as FormikField } from "formik";
import React, { useState } from "react";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import ReactSelect from "react-select";
import FieldTypes from "../../../Constraints/FieldTypes";
import LongText from "./FieldTypes/LongText";
import ShortText from "./FieldTypes/ShortText";

type Props = {
  field: FieldValues;
  onFinishEditingHandler: () => void;
  onAbortEditingHandler: () => void;
};

type FieldValues = {
  name: string;
  type?: FieldTypes;
  value?: any;
};

function FieldEditor(props: Props) {
  const { t } = useTranslation();
  const fieldTypes = Object.values(FieldTypes).map((type) => ({ value: type, label: t(type.toString()) }));
  const [fieldType, setFieldType] = useState(props.field.type ?? FieldTypes.ShortText);
  return (
    <Formik initialValues={props.field} onSubmit={(values: FieldValues, formikHelpers: FormikHelpers<FieldValues>) => {}}>
      <>
        <Row className="m-2 p-2">
          <Col>
            <Form.Label htmlFor="name">{t("fieldName")}</Form.Label>
            <FormikField id="name" name="name" className="form-control" />
          </Col>
          <Col>
            <Form.Label>{t("fieldType")}</Form.Label>
            <ReactSelect
              value={fieldTypes.find((type) => type.value == fieldType)}
              options={fieldTypes}
              onChange={(newValue) => setFieldType(newValue?.value as FieldTypes)}
            />
          </Col>
        </Row>
        <Row className="m-2 p-2">
          {(() => {
            switch (fieldType) {
              case FieldTypes.ShortText:
                return <ShortText value={props.field.value} />;
              case FieldTypes.LongText:
                return <LongText value={props.field.value} />;
              default:
                return <></>;
            }
          })()}
        </Row>
        <Row className="m-2 p-2">Field properties</Row>
        <Row className="m-2 p-2">
          <div className="d-flex justify-content-end">
            <ButtonGroup>
              <Button variant="danger" onClick={props.onAbortEditingHandler}>
                {t("discard")}
              </Button>
              <Button variant="success" onClick={props.onFinishEditingHandler}>
                {t("confirm")}
              </Button>
            </ButtonGroup>
          </div>
        </Row>
      </>
    </Formik>
  );
}

export default FieldEditor;
