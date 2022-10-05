import { Formik, FormikHelpers, FormikValues, Field as FormikField } from "formik";
import React, { useState } from "react";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import ReactSelect, { GroupBase } from "react-select";
import FieldTypes from "../../../Constraints/FieldTypes";
import FieldEditor from "./FieldEditor";
import FieldPreview from "./FieldPreview";
import LongText from "./FieldTypes/LongText";
import ShortText from "./FieldTypes/ShortText";

type Props = {
  field: FieldValues;
  editorMode?: boolean;
  onRemoveFieldHandler: () => void;
};
type FieldValues = {
  name: string;
  type?: FieldTypes;
  value?: any;
};

function Field(props: Props) {
  const [editorMode, setEditorMode] = useState(props.editorMode ?? false);
  return (
    <>
      {editorMode ? (
        <FieldEditor
          field={props.field}
          onAbortEditingHandler={() => setEditorMode(false)}
          onFinishEditingHandler={() => setEditorMode(false)}
        />
      ) : (
        <FieldPreview
          field={props.field}
          onRemoveFieldHandler={props.onRemoveFieldHandler}
          onEditFieldHandler={() => setEditorMode(true)}
        />
      )}
    </>
  );
}

export default Field;
