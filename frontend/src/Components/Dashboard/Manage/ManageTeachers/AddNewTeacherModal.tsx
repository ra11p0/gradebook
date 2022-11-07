import { Button } from "@mui/material";
import { useFormik } from "formik";
import React from "react";
import { Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import { connect } from "react-redux";
import NewTeacherRequest from "../../../../ApiClient/Schools/Definitions/Requests/NewTeacherRequest";
import ReactDatePicker from "react-datepicker";
import moment from "moment";
import getApplicationLanguageReduxProxy from "../../../../Redux/ReduxProxy/getApplicationLanguageReduxProxy";

type Props = {
  show: boolean;
  onHide: () => void;
  currentSchoolGuid?: string;
  locale: string;
};
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
  locale: getApplicationLanguageReduxProxy(state),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface formValues {
  name: string;
  surname: string;
  birthday: Date;
}
function AddNewTeacherModal(props: Props) {
  const { t } = useTranslation("addNewTeacherModal");
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
      birthday: new Date(),
    },
    validate,
    onSubmit: (values: formValues) => {
      var teacher: NewTeacherRequest = {
        Name: values.name,
        Surname: values.surname,
        Birthday: new Date(values.birthday),
      };
      SchoolsProxy.addNewTeacher(teacher, props.currentSchoolGuid!).then(props.onHide);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addTeacher")}</Modal.Title>
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
            <ReactDatePicker
              selected={formik.values.birthday}
              className="form-control"
              onChange={(evt) => {
                formik.handleChange({ target: { name: "birthday", id: "birthday", value: evt } });
              }}
              dateFormat={"P"}
              locale={props.locale}
            />
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit" variant="outlined">
            {t("addTeacher")}
          </Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(AddNewTeacherModal);
