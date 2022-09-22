import { Button } from "@mui/material";
import { Field, Form, Formik, FormikHelpers } from "formik";
import React from "react";
import { Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import * as Yup from "yup";
import SchoolsProxy from "../../../../ApiClient/Schools/SchoolsProxy";
import { connect } from "react-redux";
import Notifications from "../../../../Notifications/Notifications";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
});
const mapDispatchToProps = (dispatch: any) => ({});

type Props = {
  show: boolean;
  onHide: () => void;
  currentSchoolGuid?: string;
};
interface FormValues {
  name: string;
  description?: string;
}

function AddClassModal(props: Props) {
  const { t } = useTranslation("addNewClassModal");
  const formValuesSchema = Yup.object().shape({
    name: Yup.string()
      .required(t("nameIsRequired"))
      .min(2, t("nameTooShort"))
      .max(50, t("nameTooLong")),
    description: Yup.string().optional(),
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addClass")}</Modal.Title>
      </Modal.Header>
      <Formik
        initialValues={{
          name: "",
          description: "",
        }}
        validationSchema={formValuesSchema}
        onSubmit={(values: FormValues) => {
          SchoolsProxy.addNewClass(values, props.currentSchoolGuid!)
            .then(() => {
              props.onHide();
            })
            .catch(Notifications.showApiError);
        }}
      >
        {({ errors, touched }) => (
          <Form>
            <Modal.Body>
              <div className="m-1 p-1">
                <label htmlFor="name">{t("name")}</label>
                <Field id="name" name="name" className="form-control" />
                {errors.name && touched.name && (
                  <div className="invalid-feedback d-block">{errors.name}</div>
                )}
              </div>
              <div className="m-1 p-1">
                <label htmlFor="description">{t("description")}</label>
                <Field
                  id="description"
                  name="description"
                  className="form-control"
                />
                {errors.description && touched.description && (
                  <div className="invalid-feedback d-block">
                    {errors.description}
                  </div>
                )}
              </div>
            </Modal.Body>
            <Modal.Footer>
              <Button type="submit" variant="outlined">
                {t("addClass")}
              </Button>
            </Modal.Footer>
          </Form>
        )}
      </Formik>
    </Modal>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(AddClassModal);
