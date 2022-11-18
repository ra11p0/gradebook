import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useFormik } from "formik";
import { useTranslation } from "react-i18next";
import { Button, Col, Row } from "react-bootstrap";
import InvitationsProxy from "../../../ApiClient/Invitations/InvitationsProxy";
import moment from "moment";
import PeopleProxy from "../../../ApiClient/People/PeopleProxy";
import Notifications from "../../../Notifications/Notifications";
import AccountProxy from "../../../ApiClient/Accounts/AccountsProxy";
import GetAccessibleSchoolsResponse from "../../../ApiClient/Accounts/Definitions/Responses/GetAccessibleSchoolsResponse";
import getCurrentUserIdReduxProxy from "../../../Redux/ReduxQueries/account/getCurrentUserIdRedux";
import setSchoolsListReduxWrapper from "../../../Redux/ReduxCommands/account/setSchoolsListRedux";
import setLoginReduxWrapper from "../../../Redux/ReduxCommands/account/setLoginRedux";
import { store } from "../../../store";
import getSessionRedux from '../../../Redux/ReduxQueries/account/getSessionRedux'

const mapStateToProps = (state: any) => ({
  userId: getCurrentUserIdReduxProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolsList: (schoolsList: GetAccessibleSchoolsResponse[]) => setSchoolsListReduxWrapper(dispatch, { schoolsList }),
});

interface ActivateStudentFormProps {
  defaultOnBackHandler: () => void;
  setSchoolsList?: (schoolsList: GetAccessibleSchoolsResponse[]) => void;
  userId?: string;
  onSubmit?: () => void;
}

interface ActivateStudentFormValues {
  accessCode: string;
}

const ActivateStudentForm = (props: ActivateStudentFormProps): ReactElement => {
  const { t } = useTranslation("ActivateStudent");

  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [birthday, setBirthday] = useState("");

  const validate = (values: ActivateStudentFormValues) => {
    const errors: any = {};
    if (values.accessCode.length != 6) {
      errors.accessCode = t("wrongAccessCodeLength");
    }
    return errors;
  };

  const formik = useFormik({
    initialValues: {
      accessCode: "",
    },
    validate,
    onSubmit: (values: ActivateStudentFormValues) => {
      PeopleProxy.activatePerson(values.accessCode)
        .then(() => {
          AccountProxy.getAccessibleSchools(props.userId!).then((schoolsResponse) => {
            const session = getSessionRedux();
            if (!session) return;
            setLoginReduxWrapper(store.dispatch, {
              accessToken: session.accessToken,
              refreshToken: session.refreshToken,
            });
            if (props.onSubmit) props.onSubmit();
          });
        })
        .catch(Notifications.showApiError);
    },
  });

  const handleAccessCodeChange = function (e: any) {
    if (e.target.value.length == 6) {
      InvitationsProxy.getInvitationDetailsForStudent(e.target.value)
        .then((resp) => {
          var data = resp.data;
          setName(data.person.name);
          setSurname(data.person.surname);
          setBirthday(moment.utc(data.person.birthday).local().format("L"));
        })
        .catch((err) => {
          setName("");
          setSurname("");
          setBirthday("");
        })
        .catch(Notifications.showApiError);
    }
  };

  return (
    <div className="card m-3 p-3">
      <Button onClick={props.defaultOnBackHandler} variant={"link"}>
        {t("back")}
      </Button>
      <Row className="text-center">
        <div className="h4">{t("ActivateStudent")}</div>
      </Row>
      <form onSubmit={formik.handleSubmit}>
        <div className="m-1 p-1">
          <label htmlFor="accessCode">{t("accessCode")}</label>
          <input
            className="form-control"
            id="accessCode"
            name="accessCode"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.accessCode}
            onInput={handleAccessCodeChange}
          />
          {formik.errors.accessCode && formik.touched.accessCode ? (
            <div className="invalid-feedback d-block">{formik.errors.accessCode}</div>
          ) : null}
        </div>
        <Row>
          <Col>
            <div className="m-1 p-1">
              <label>{t("name")}</label>
              <input className="form-control" type="text" defaultValue={name} disabled />
            </div>
          </Col>
          <Col>
            <div className="m-1 p-1">
              <label>{t("surname")}</label>
              <input className="form-control" type="text" defaultValue={surname} disabled />
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <div className="m-1 p-1">
              <label>{t("birthday")}</label>
              <input className="form-control" type="text" defaultValue={birthday} disabled />
            </div>
          </Col>
        </Row>

        <Button variant="outline-primary" type="submit">
          {t("confirmInformation")}
        </Button>
      </form>
    </div>
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(ActivateStudentForm);
