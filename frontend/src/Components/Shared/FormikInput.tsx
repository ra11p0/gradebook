import React from "react";
import { Stack } from "react-bootstrap";
import FormikValidationLabel from './FormikValidationLabel'

type Props = {
  name: string;
  label?: string;
  type?: string;
  formik: {
    handleChange: (evt: React.ChangeEvent<any>) => void;
    handleBlur: (evt: React.ChangeEvent<any>) => void;
    values: any;
    errors: any;
    touched: any;
  };
  onChange?: (evt: React.ChangeEvent<HTMLInputElement>) => void,
  showErrorsDespiteTouching?: boolean
};

function FormikInput(props: Props) {
  return (
    <Stack>
      <label htmlFor={props.name}>{props.label ?? props.name}</label>
      <input
        className="form-control"
        id={props.name}
        name={props.name}
        type={props.type ?? "text"}
        onChange={(evt) => {
          props.formik.handleChange(evt)
          if (props.onChange) props.onChange(evt);
        }}
        onBlur={evt => { props.formik.handleBlur(evt) }}
        value={props.formik.values[props.name]}
      />
      <FormikValidationLabel {...props} showDespiteTouching={props.showErrorsDespiteTouching} />
    </Stack>
  );
}

export default FormikInput;
