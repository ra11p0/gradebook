import { Formik, FormikHelpers, Field as FormikField, useFormik } from "formik";
import React, { useState } from "react";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import ReactSelect from "react-select";
import FieldTypes from "../../../Constraints/FieldTypes";
import Field from "../../../Interfaces/Common/Field";
import ReduxSetField from "../../../Redux/ReduxSet/ReduxSetField";
import LongText from "./FieldTypes/LongText";
import ShortText from "./FieldTypes/ShortText";
import * as Yup from "yup";

type Props = {
  field: Field;
  onFinishEditingHandler: () => void;
  onAbortEditingHandler: (uuid: string) => void;
};

function FieldEditor(props: Props) {
  const { t } = useTranslation();
  const types = Object.values(FieldTypes).map((value) => ({ value, label: t(value.toString()) }));
  const formik = useFormik({
    initialValues: {
      name: props.field.name,
      type: props.field.type ?? FieldTypes.ShortText,
    },
    validationSchema: Yup.object().shape({
      name: Yup.string().required(t("fieldNameIsRequired")),
      description: Yup.string(),
    }),
    onSubmit(values, formikHelpers) {
      ReduxSetField({ ...props.field, name: values.name, type: values.type });
      props.onFinishEditingHandler();
    },
  });
  return (
    <Form onSubmit={formik.handleSubmit} className="shadow m-2 p-2">
      <Row className="m-2 p-2">
        <Col>
          <Form.Label htmlFor="name">{t("fieldName")}</Form.Label>
          <Form.Control
            type="text"
            id="name"
            name="name"
            value={formik.values.name}
            onChange={formik.handleChange}
            isInvalid={formik.errors.name !== undefined}
          />
          <Form.Control.Feedback type="invalid">{formik.errors.name}</Form.Control.Feedback>
        </Col>
        <Col>
          <Form.Label>{t("fieldType")}</Form.Label>
          <ReactSelect
            value={types.find((t) => t.value == formik.values.type)}
            options={types}
            onChange={(newValue) => formik.handleChange({ target: { value: newValue?.value, id: "type", name: "type" } })}
          />
        </Col>
      </Row>
      <Row className="m-2 p-2">
        {(() => {
          switch (formik.values.type) {
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
            <Button variant="danger" onClick={() => props.onAbortEditingHandler(props.field.uuid)}>
              {t("discard")}
            </Button>
            <Button variant="success" type="submit">
              {t("confirm")}
            </Button>
          </ButtonGroup>
        </div>
      </Row>
    </Form>
  );
}

export default FieldEditor;
