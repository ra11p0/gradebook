import { useFormik } from "formik";
import { useEffect } from "react";
import { Form } from "react-bootstrap";
import Field from "../../../../Interfaces/Common/Field";
import * as Yup from "yup";
import { useTranslation } from "react-i18next";
import ValidationTypes from "../../../../Constraints/ValidationTypes";

function LongText(props: Field) {
  const { t } = useTranslation();
  const formik = useFormik({
    initialValues: {
      value: props.value as string ?? ''
    },
    validationSchema: Yup.object().shape({
      value: (() => {
        let schema = Yup.string();
        if (props.isRequired) schema = schema.required(t('fieldRequired'))
        return schema
      })()
    }),
    onSubmit: () => { }
  });
  useEffect(() => {
    if (props.validationKey == ValidationTypes.InitialValidate) {
      formik.validateForm().then((errors) => {
        if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.value, errors })
      });
    }
    else if (props.validationKey == ValidationTypes.SubmitValidate) {
      formik.submitForm();
    }
  }, [props.validationKey]);
  return (
    <>
      <Form.Control
        as={"textarea"}
        name='value'
        id='value'
        value={formik.values.value}
        onChange={(evt) => {
          formik.handleChange(evt);
        }}
        onBlur={() => {
          if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.value, errors: formik.errors })
        }}
        isInvalid={formik.errors.value != undefined && formik.touched.value}
      />
      <Form.Control.Feedback type="invalid" >
        {formik.touched.value && formik.errors.value as string}
      </Form.Control.Feedback>
    </>
  );
}

export default LongText;
