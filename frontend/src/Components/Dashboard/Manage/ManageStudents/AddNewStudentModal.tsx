import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button, Modal } from "react-bootstrap";
import { useFormik } from "formik";
import NewStudentRequest from "../../../../ApiClient/Schools/Definitions/Requests/NewStudentRequest";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import { currentSchoolProxy } from "../../../../Redux/ReduxProxy/getCurrentSchoolReduxProxy";
const mapStateToProps = (state: any) => ({
  currentSchool: currentSchoolProxy(state),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface formValues {
  name: string;
  surname: string;
  birthday: any;
}
interface AddNewStudentModalProps {
  show: boolean;
  onHide: () => void;
  currentSchool: any;
}
const AddNewStudentModal = (props: AddNewStudentModalProps): ReactElement => {
  const { t } = useTranslation("addNewStudentModal");
  const validate = (values: formValues) => {
    const errors: any = {};
    if (values.name.length < 3) errors.name = t("nameInvalid");
    if (values.surname.length < 3) errors.surname = t("surnameInvalid");
    return errors;
  };
  const formik = useFormik({
    initialValues: {
      name: "",
      surname: "",
      birthday: new Date().toDateString(),
    },
    validate,
    onSubmit: (values: formValues) => {
      var student: NewStudentRequest = {
        Name: values.name,
        Surname: values.surname,
        Birthday: new Date(values.birthday),
      };
      SchoolsProxy.addNewStudent(student, props.currentSchool.schoolGuid!).then(props.onHide);
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
          <div className="m-1 p-1">
            <label htmlFor="surname">{t("surname")}</label>
            <input
              className="form-control"
              id="surname"
              name="surname"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.surname}
            />
            {formik.errors.surname && formik.touched.surname ? (
              <div className="invalid-feedback d-block">{formik.errors.surname}</div>
            ) : null}
          </div>
          <div className="m-1 p-1">
            <label htmlFor="birthday">{t("birthday")}</label>
            <input
              className="form-control"
              id="birthday"
              name="birthday"
              type="date"
              onChange={formik.handleChange}
              value={formik.values.birthday}
            />
            {formik.errors.birthday && formik.touched.birthday ? (
              <div className="invalid-feedback d-block">{formik.errors.birthday as string}</div>
            ) : null}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit">{t("addStudent")}</Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(AddNewStudentModal);
