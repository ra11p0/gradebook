import { useFormik } from "formik";
import { useEffect } from "react";
import { Col, Form, Row } from "react-bootstrap";
import Field from "../../../../Interfaces/Common/Field";
import { useTranslation } from "react-i18next";
import ValidationTypes from "../../../../Constraints/ValidationTypes";

function Checkbox(props: Field) {
    const { t } = useTranslation();
    const formik = useFormik({
        initialValues: {
            values: props.value ?? (props.labels ?? []).map(() => false)
        },
        validate: (values: { values: boolean[] }) => {
            const errors: any = {};
            const vals = values.values ?? [];
            if (props.isRequired && !vals.find(e => e)) errors.values = t('atLeastOneRequired')
            return errors;
        },
        onSubmit: () => { }
    })
    useEffect(() => {
        if (props.validationKey == ValidationTypes.InitialValidate) {
            formik.validateForm().then((errors) => {
                if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.values, errors });
            });
        }
        if (props.validationKey == ValidationTypes.SubmitValidate) {
            formik.submitForm();
        }
    }, [props.validationKey]);
    return (
        <>
            {
                (props.labels ?? []).map((_, i) =>
                    <Row key={i}>
                        <Col>
                            <Form.Check
                                key={i}
                                type="checkbox"
                                checked={formik.values.values[i] ?? false}
                                onChange={(newValue: { target: { checked: boolean; }; }) => {
                                    const newValues = (props.distinctValues ?? false) ? formik.values.values.map(() => false) : formik.values.values.slice();
                                    newValues[i] = newValue.target.checked;
                                    formik.handleChange({ target: { value: newValues, id: 'values', name: 'values' } });
                                }}
                                label={props.labels![i]}
                                isInvalid={formik.errors.values != undefined && formik.touched.values}
                                onBlur={() => { if (props.onBlur) props.onBlur({ target: props, updatedValue: formik.values.values, errors: formik.errors }) }}
                            />
                        </Col>
                    </Row>)
            }
            <Form.Control.Feedback type="invalid" className="d-inline" >
                {formik.touched.values && formik.errors.values as string}
            </Form.Control.Feedback>
        </>
    );
}

export default Checkbox;
