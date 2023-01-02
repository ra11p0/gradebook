import _ from 'lodash';
import React, { ReactElement } from 'react';
import { Form } from 'react-bootstrap';
import FormikDatePicker, { FormikDatePickerProps } from './FormikDatePicker';
import FormikValidationLabel from './FormikValidationLabel';

interface Props {
  testId?: string;
  id?: string;
  name: string;
  label?: string;
  type?: 'date' | 'switch' | 'password';
  formik: {
    setFieldValue: (fieldName: string, value: any) => void;
    handleChange: (evt: React.ChangeEvent<any>) => void;
    handleBlur: (evt: React.ChangeEvent<any>) => void;
    values: any;
    errors: any;
    touched: any;
  };
  onChange?: (evt: React.ChangeEvent<HTMLInputElement>) => void;
  showErrorsDespiteTouching?: boolean;
  hideValidation?: boolean;
}

function FormikInput(
  props: Props & ({} | FormikDatePickerProps)
): ReactElement {
  return (
    <Form.Group className="m-1 p-1">
      {(() => {
        switch (props.type) {
          case 'switch':
            return (
              <>
                <Form.Check
                  data-testid={props.testId ?? props.id ?? props.name}
                  type="switch"
                  id={props.id ?? props.name}
                  name={props.name}
                  label={props.label ?? props.name}
                  checked={_.get(props.formik.values, props.name)}
                  onChange={(evt) => {
                    props.formik.handleChange(evt);
                    if (props.onChange) props.onChange(evt);
                  }}
                  onBlur={(evt) => {
                    props.formik.handleBlur(evt);
                  }}
                  isInvalid={
                    _.get(props.formik.errors, props.name) &&
                    _.get(props.formik.touched, props.name)
                  }
                  isValid={
                    !_.get(props.formik.errors, props.name) &&
                    _.get(props.formik.touched, props.name)
                  }
                />
              </>
            );
          case 'date':
            return (
              <>
                <Form.Label htmlFor={props.name}>
                  {props.label ?? props.name}
                </Form.Label>
                <FormikDatePicker {...props} />
              </>
            );
          default:
            return (
              <>
                <Form.Label htmlFor={props.name}>
                  {props.label ?? props.name}
                </Form.Label>
                <Form.Control
                  data-testid={props.testId ?? props.id ?? props.name}
                  className="form-control"
                  id={props.id ?? props.name}
                  name={props.name}
                  type={props.type ?? 'text'}
                  onChange={(evt: any) => {
                    props.formik.handleChange(evt);
                    if (props.onChange) props.onChange(evt);
                  }}
                  isInvalid={
                    _.get(props.formik.errors, props.name) &&
                    _.get(props.formik.touched, props.name)
                  }
                  isValid={
                    !_.get(props.formik.errors, props.name) &&
                    _.get(props.formik.touched, props.name)
                  }
                  onBlur={(evt: any) => {
                    props.formik.handleBlur(evt);
                  }}
                  value={_.get(props.formik.values, props.name) ?? ''}
                />
              </>
            );
        }
      })()}
      {!props.hideValidation && (
        <FormikValidationLabel
          {...props}
          showDespiteTouching={props.showErrorsDespiteTouching}
        />
      )}
    </Form.Group>
  );
}

export default FormikInput;
