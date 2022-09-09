import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { useFormik } from "formik";
import { Button } from "react-bootstrap";
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface RegisterAdministratorPersonProps {
  onSubmit: (values: RegisterAdministratorPersonValues) => void;
  name?: string;
  surname?: string;
  birthday?: string;
}
interface RegisterAdministratorPersonValues {
  name: string;
  surname: string;
  birthday: string;
}
const RegisterAdministratorPerson = (
  props: RegisterAdministratorPersonProps
): ReactElement => {
  const { t } = useTranslation("registerAdministratorPerson");
  const validate = (values: RegisterAdministratorPersonValues) => {
    const errors: any = {};
    if (values.name.length < 3) {
      errors.name = t("invalidName");
    }
    if (values.surname.length < 3) {
      errors.surname = t("invalidSurname");
    }
    return errors;
  };
  const formik = useFormik({
    initialValues: {
      name: props.name ?? "",
      surname: props.surname ?? "",
      birthday: props.birthday ?? new Date().toDateString(),
    },
    validate,
    onSubmit: props.onSubmit,
  });
  return (
    <form onSubmit={formik.handleSubmit}>
      <div className="m-1 p-1">
        <label htmlFor="name">{t("name")}</label>
        <input
          className="form-control"
          id="name"
          name="name"
          type="text"
          onChange={formik.handleChange}
          value={formik.values.name}
        />
        {formik.errors.name && formik.touched.name ? (
          <div className="invalid-feedback d-block">{formik.errors.name}</div>
        ) : null}
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
          <div className="invalid-feedback d-block">
            {formik.errors.surname}
          </div>
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
          <div className="invalid-feedback d-block">
            {formik.errors.birthday}
          </div>
        ) : null}
      </div>
      <div className="m-1 p-1 d-flex justify-content-end">
        <Button variant="outline-primary" type="submit">
          {t("nextStep")}
        </Button>
      </div>
    </form>
  );
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterAdministratorPerson);
export type { RegisterAdministratorPersonValues };
