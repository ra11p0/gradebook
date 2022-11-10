import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button, Modal } from "react-bootstrap";
import { useFormik } from "formik";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import Notifications from "../../../../Notifications/Notifications";
import NewSubjectRequest from "../../../../ApiClient/Schools/Definitions/Requests/NewSubjectRequest";

interface formValues {
  name: string;
}
interface Props {
  show: boolean;
  onHide: () => void;
}
const NewSubjectModal = (props: Props): ReactElement => {
  const { t } = useTranslation("addNewStudentModal");
  const validate = (values: formValues) => {
    const errors: any = {};
    if (values.name.length < 3) errors.name = t("nameInvalid");
    return errors;
  };
  useEffect(() => {
    formik.resetForm();
  }, [props.show]);
  const formik = useFormik({
    initialValues: {
      name: "",
      surname: "",
      birthday: new Date(),
    },
    validate,
    onSubmit: (values: formValues) => {
      var subject: NewSubjectRequest = {
        name: values.name,
      };
      SchoolsProxy.subjects.addNewSubject(subject).then(props.onHide).catch(Notifications.showApiError);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addStudent")}</Modal.Title>
      </Modal.Header>
      <form onSubmit={formik.handleSubmit}>
        <Modal.Body>
          <div className="m-1 p-1">
            <label htmlFor="name">{t("name")}</label>
            <input className="form-control" id="name" name="name" type="text" onChange={formik.handleChange} value={formik.values.name} />
            {formik.errors.name && formik.touched.name ? <div className="invalid-feedback d-block">{formik.errors.name}</div> : null}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit">{t("addStudent")}</Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};
export default connect(
  () => ({}),
  () => ({})
)(NewSubjectModal);
