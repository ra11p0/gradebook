import { useFormik } from "formik";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import ReactSelect from "react-select";
import FieldTypes from "../../../Constraints/FieldTypes";
import Field from "../../../Interfaces/Common/Field";
import ReduxSetField from "../../../Redux/ReduxSet/ReduxSetField";
import * as Yup from "yup";
import DynamicTextListInput from "../../Common/DynamicTextListInput";

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
      description: props.field.description ?? "",
      labels: props.field.labels ?? [''],
      useDescription: props.field.description != null && props.field.description != '',
      distinctValues: props.field.distinctValues ?? false,
      forbidPast: props.field.forbidPast ?? false,
      forbidFuture: props.field.forbidFuture ?? false,
      isRequired: props.field.isRequired ?? false
    },
    validationSchema: Yup.object().shape({
      name: Yup.string().required(t("fieldNameIsRequired")),
      description: Yup.string(),
    }),
    onSubmit(values) {
      ReduxSetField({
        ...props.field,
        name: values.name,
        type: values.type,
        labels: values.labels,
        description: values.useDescription ? values.description : '',
        distinctValues: values.distinctValues,
        forbidPast: values.forbidPast,
        forbidFuture: values.forbidFuture,
        isRequired: values.isRequired
      });
      props.onFinishEditingHandler();
    },
  });
  return (
    <Form onSubmit={formik.handleSubmit} className="border rounded-3 shadow">
      <Row className="m-2 p-2">
        <Col>
          <Form.Label htmlFor="name">{t("fieldName")}</Form.Label>
          <Form.Control
            type="text"
            id="name"
            name="name"
            value={formik.values.name}
            onChange={formik.handleChange}
            isInvalid={formik.touched.name && formik.errors.name !== undefined}
          />
          <Form.Control.Feedback type="invalid">{formik.touched.name && formik.errors.name}</Form.Control.Feedback>
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
      <div className="m-2 p-2">
        <Form.Text>
          {t('options')}
        </Form.Text>
        <Row>
          <Col>
            {
              //  field options, different for different fields
              formik.values.type == FieldTypes.Checkbox &&
              <DynamicTextListInput
                name='labels'
                id='labels'
                values={formik.values.labels}
                onChange={formik.handleChange} />
            }
            {
              formik.values.type == FieldTypes.Checkbox &&
              <Form.Check
                id='distinctValues'
                name='distinctValues'
                checked={formik.values.distinctValues}
                onChange={formik.handleChange}
                label={t('distinctValues')}
              />
            }
            {
              formik.values.type == FieldTypes.Date &&
              <>
                <Form.Check
                  id='forbidPast'
                  name='forbidPast'
                  checked={formik.values.forbidPast}
                  onChange={formik.handleChange}
                  label={t('forbidPast')}
                />
                <Form.Check
                  id='forbidFuture'
                  name='forbidFuture'
                  checked={formik.values.forbidFuture}
                  onChange={formik.handleChange}
                  label={t('forbidFuture')}
                />
              </>
            }
            {
              <Form.Check
                id='isRequired'
                name='isRequired'
                checked={formik.values.isRequired}
                onChange={formik.handleChange}
                label={t('isRequired')}
              />
            }
          </Col>
        </Row>
        <Row>
          <Col>
            <Form.Check
              id='useDescription'
              name='useDescription'
              checked={formik.values.useDescription}
              onChange={formik.handleChange}
              label={t('useDescription')}
            />
            {
              formik.values.useDescription && <Form.Control
                as={"textarea"}
                name='description'
                id='description'
                value={formik.values.description}
                onChange={formik.handleChange} />
            }
          </Col>
        </Row>
      </div>
      <Row className="m-2 p-2">
        <Col className="d-flex justify-content-end">
          <ButtonGroup>
            <Button variant="danger" onClick={() => props.onAbortEditingHandler(props.field.uuid)}>
              {t("discard")}
            </Button>
            <Button variant="success" type="submit">
              {t("confirm")}
            </Button>
          </ButtonGroup>
        </Col>
      </Row>
    </Form>
  );
}

export default FieldEditor;
