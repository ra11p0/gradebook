import React from "react";

type Props = {
  name: string;
  label?: string;
  type?: string;
  formik: {
    handleChange: (evt: React.ChangeEvent<any>) => void;
    values: any;
    errors: any;
    touched: any;
  };
  onChange?: (evt: React.ChangeEvent<HTMLInputElement>) => void
};

function FormikInput(props: Props) {
  return (
    <div>
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
        value={props.formik.values[props.name]}
      />
      {props.formik.errors[props.name] && props.formik.touched[props.name] ? (
        <div className="invalid-feedback d-block">{props.formik.errors.name}</div>
      ) : null}
    </div>
  );
}

export default FormikInput;
