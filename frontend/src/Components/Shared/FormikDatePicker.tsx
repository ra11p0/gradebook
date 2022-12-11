import _ from 'lodash';
import React, { ReactElement } from 'react';
import ReactDatePicker from 'react-datepicker';
import { connect } from 'react-redux';
import getApplicationLanguageRedux from '../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import { GlobalState } from '../../store';

interface Props {
  locale: string;
  testId?: string;
  name: string;
  label?: string;
  formik: {
    setFieldValue: (fieldName: string, value: Date | null) => void;
    handleChange: (evt: React.ChangeEvent<any>) => void;
    handleBlur: (evt: React.ChangeEvent<any>) => void;
    values: any;
    errors: any;
    touched: any;
  };
  onChange?: (evt: React.ChangeEvent<HTMLInputElement>) => void;
}

function FormikDatePicker({ formik, locale, name }: Props): ReactElement {
  return (
    <>
      <ReactDatePicker
        selected={_.get(formik.values, name)}
        className="form-control"
        onChange={(evt) => {
          formik.setFieldValue(name, evt);
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
