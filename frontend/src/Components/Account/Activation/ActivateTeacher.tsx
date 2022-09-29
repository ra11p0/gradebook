import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useFormik } from "formik";
import { useTranslation } from "react-i18next";
import { Button, Row } from "react-bootstrap";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface ActivateTeacherFormProps {
  defaultOnBackHandler: () => void;
  onSubmit?: () => void;
}

interface ActivateTeacherFormValues {}

const ActivateTeacherForm = (props: ActivateTeacherFormProps): ReactElement => {
  const { t } = useTranslation();

  const validate = (values: ActivateTeacherFormValues) => {
    const errors: any = {};
    return errors;
  };

  const formik = useFormik({
    initialValues: {},
    validate,
    onSubmit: (values: ActivateTeacherFormValues) => {
      if (props.onSubmit) props.onSubmit();
    },
  });

  return (
    <div className="card m-3 p-3">
      <Button onClick={props.defaultOnBackHandler} variant={"link"}>
        {t("back")}
      </Button>
      <Row className="text-center">
        <div>Register teacher</div>
      </Row>
      <form onSubmit={formik.handleSubmit}>
        <Button type="submit">{t("submit")}</Button>
      </form>
    </div>
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(ActivateTeacherForm);