import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useFormik } from "formik";
import { useTranslation } from "react-i18next";
import { Button, Row } from "react-bootstrap";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface RegisterTeacherFormProps {
  defaultOnBackHandler: () => void;
}

interface RegisterTeacherFormValues {}

const RegisterTeacherForm = (props: RegisterTeacherFormProps): ReactElement => {
  const { t } = useTranslation();

  const validate = (values: RegisterTeacherFormValues) => {
    const errors: any = {};
    return errors;
  };

  const formik = useFormik({
    initialValues: {},
    validate,
    onSubmit: (values: RegisterTeacherFormValues) => {},
  });

  return (
    <div className="card m-3 p-3">
      <Button onClick={props.defaultOnBackHandler} variant={"link"}>
        {t("back")}
      </Button>
      <Row className="text-center">
        <div>Register teacher</div>
      </Row>
      <form onSubmit={formik.handleSubmit}></form>
    </div>
  );
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterTeacherForm);
