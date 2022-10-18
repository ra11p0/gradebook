import { useFormik } from "formik";
import { useEffect } from "react";
import { Form } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import * as Yup from "yup";
import ValidationTypes from "../../../../Constraints/ValidationTypes";
import Field from "../../../../Interfaces/Common/Field";

function Email(props: Field) {
    const { t } = useTranslation();
    const formik = useFormik({
        initialValues: {
            email: (props.value as string) ?? ""
        },
        validateOnChange: true,
        validationSchema: Yup.object().shape({
            email: (() => {
                let schema = Yup.string().email(t('wrongEmailValue'));
                if (props.isRequired) schema = schema.required(t('fieldRequired'));
                return schema;
            })()
        }),
        onSubmit: () => { }
    })
    useEffect(() => {
        if (props.validationKey == ValidationTypes.InitialValidate) {
            formik.validateForm().then((errors) => {
                if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.email, errors });
            });
        }
        else if (props.validationKey == ValidationTypes.SubmitValidate) {
            formik.submitForm();
        }
    }, [props.validationKey]);
    return (
        <>
            <Form.Control
                name='email'
                id='email'
                value={formik.values.email}
                onChange={(evt) => {
                    formik.handleChange(evt);
                }}
                onBlur={() => {
                    if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.email, errors: formik.errors });
                }}
                isInvalid={formik.errors.email != undefined && formik.touched.email}
            />
            <Form.Control.Feedback type="invalid">{formik.touched.email && formik.errors.email}</Form.Control.Feedback>
        </>
    );
}

export default Email;
