import { Button } from "@mui/material";
import { useFormik } from "formik";
import React from "react";
import { Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";

interface AddNewSchoolModalProps {
  show: boolean;
  onHide: () => void;
}
interface formValues {
  name: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  postalCode: string;
}
function AddNewSchoolModal(props: AddNewSchoolModalProps) {
  const { t } = useTranslation("registerAdministratorSchool");
  const validate = (values: formValues) => {
    const errors: any = {};
    if (values.addressLine1.length < 5)
      errors.addressLine1 = t("addressTooShort");
    if (values.name.length < 2) errors.name = t("nameTooShort");
    if (values.city.length < 3) errors.city = t("cityNameTooShort");
    if (values.postalCode.length < 3)
      errors.postalCode = t("postalCodeTooShort");
    return errors;
  };
  const formik = useFormik({
    initialValues: {
      name: "",
      addressLine1: "",
      addressLine2: "",
      city: "",
      postalCode: "",
    },
    validate,
    onSubmit: (values: formValues) => {
      console.dir(values);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addSchool")}</Modal.Title>
      </Modal.Header>
      <form onSubmit={formik.handleSubmit}>
        <Modal.Body>
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
              <div className="invalid-feedback d-block">
                {formik.errors.name}
              </div>
            ) : null}
          </div>
          <div className="m-1 p-1">
            <label htmlFor="addressLine1">{t("address")}</label>
            <input
              className="form-control"
              id="addressLine1"
              name="addressLine1"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.addressLine1}
            />
            {formik.errors.addressLine1 && formik.touched.addressLine1 ? (
              <div className="invalid-feedback d-block">
                {formik.errors.addressLine1}
              </div>
            ) : null}
          </div>
          <div className="m-1 p-1">
            <label htmlFor="addressLine2">{t("address")}</label>
            <input
              className="form-control"
              id="addressLine2"
              name="addressLine2"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.addressLine2}
            />
            {formik.errors.addressLine2 && formik.touched.addressLine2 ? (
              <div className="invalid-feedback d-block">
                {formik.errors.addressLine2}
              </div>
            ) : null}
          </div>
          <div className="m-1 p-1">
            <label htmlFor="postalCode">{t("postalCode")}</label>
            <input
              className="form-control"
              id="postalCode"
              name="postalCode"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.postalCode}
            />
            {formik.errors.postalCode && formik.touched.postalCode ? (
              <div className="invalid-feedback d-block">
                {formik.errors.postalCode}
              </div>
            ) : null}
          </div>
          <div className="m-1 p-1">
            <label htmlFor="city">{t("city")}</label>
            <input
              className="form-control"
              id="city"
              name="city"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.city}
            />
            {formik.errors.city && formik.touched.city ? (
              <div className="invalid-feedback d-block">
                {formik.errors.city}
              </div>
            ) : null}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit" variant="outlined">
            {t("addSchool")}
          </Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
}

export default AddNewSchoolModal;
