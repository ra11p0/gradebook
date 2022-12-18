import _ from 'lodash';
import React, { ReactElement } from 'react';
import ReactDatePicker from 'react-datepicker';
import { connect } from 'react-redux';
import getApplicationLanguageRedux from '../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import { GlobalState } from '../../store';

export interface FormikDatePickerProps {
  minDate?: Date;
  maxDate?: Date;
  locale: string;
  testId?: string;
  name: string;
  label?: string;
  formik: {
    setFieldValue: (fieldName: string, value: Date | undefined) => void;
    handleChange: (evt: React.ChangeEvent<any>) => void;
    handleBlur: (evt: React.ChangeEvent<any>) => void;
    values: any;
    errors: any;
    touched: any;
  };
  onChange?: (evt: React.ChangeEvent<HTMLInputElement>) => void;
}

function FormikDatePicker({
  formik,
  locale,
  name,
  testId,
  minDate,
  maxDate,
}: FormikDatePickerProps): ReactElement {
  return (
    <>
      <ReactDatePicker
        minDate={minDate}
        maxDate={maxDate}
        data-testid={testId}
        id={name}
        name={name}
        selected={_.get(formik.values, name)}
        className={`form-control ${
          _.get(formik.errors, name) && _.get(formik.touched, name)
            ? 'is-invalid'
            : ''
        } ${
          !_.get(formik.errors, name) && _.get(formik.touched, name)
            ? 'is-valid'
            : ''
        }`}
        onChange={(evt) => {
          if (evt) {
            formik.setFieldValue(name, evt);
            return;
          }
          formik.setFieldValue(name, undefined);
        }}
        onBlur={(evt) => {
          formik.handleBlur(evt);
        }}
        locale={locale}
        dateFormat="P"
      />
    </>
  );
}

export default connect(
  (state: GlobalState) => ({
    locale: getApplicationLanguageRedux(state),
  }),
  () => ({})
)(FormikDatePicker);
