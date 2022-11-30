import _ from 'lodash';
import React, { ReactElement } from 'react';
import { Collapse } from 'react-bootstrap';

interface Props {
  name: string;
  formik: {
    errors: any;
    touched: any;
  };
  showDespiteTouching?: boolean;
}
function FormikValidationLabel(props: Props): ReactElement {
  const isOpen = (): boolean =>
    !!(
      _.get(props.formik.errors, props.name) &&
      (props.showDespiteTouching ?? _.get(props.formik.touched, props.name))
    );
  return (
    <Collapse in={isOpen()}>
      <div className="invalid-feedback d-block">
        {typeof _.get(props.formik.errors, props.name) === 'string' &&
          isOpen() && <> {_.get(props.formik.errors, props.name)}</>}
      </div>
    </Collapse>
  );
}

export default FormikValidationLabel;
