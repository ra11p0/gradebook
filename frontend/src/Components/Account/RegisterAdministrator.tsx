import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useFormik } from "formik";
import { useTranslation } from "react-i18next";
import { Button, Row } from "react-bootstrap";
import RegisterAdministratorPerson, {
  RegisterAdministratorPersonValues,
} from "./RegisterAdministratorPerson";
import RegisterAdministratorSchool, {
  RegisterAdministratorSchoolValues,
} from "./RegisterAdministratorSchool";
import AdministratorsProxy from "../../ApiClient/Administrators/AdministratorsProxy";
import AccountProxy from "../../ApiClient/Account/AccountProxy";
import { refreshUser } from "../../Actions/Account/accountActions";
import { store } from "../../store";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

interface RegisterAdministratorFormProps {
  defaultOnBackHandler: () => void;
}

const activateWithoutSchool = (person: RegisterAdministratorPersonValues) => {
  AdministratorsProxy.newAdministrator(person).then((response) => {
    AccountProxy.getMe().then((meResponse) => {
      store.dispatch({
        ...refreshUser,
        roles: meResponse.data.roles,
        userId: meResponse.data.id,
        personGuid: meResponse.data.personGuid,
      });
    });
  });
};

const activateWithSchool = (
  person: RegisterAdministratorPersonValues,
  school: RegisterAdministratorSchoolValues
) => {
  AdministratorsProxy.newAdministratorWithSchool(person, school).then((e) => {
    AccountProxy.getMe().then((meResponse) => {
      store.dispatch({
        ...refreshUser,
        roles: meResponse.data.roles,
        userId: meResponse.data.id,
        personGuid: meResponse.data.personGuid,
      });
    });
  });
};

const RegisterAdministratorForm = (
  props: RegisterAdministratorFormProps
): ReactElement => {
  const { t } = useTranslation("registerAdministrator");
  const [person, setPerson] =
    useState<RegisterAdministratorPersonValues | null>(null);
  const [school, setSchool] =
    useState<RegisterAdministratorSchoolValues | null>(null);
  const [showNewSchoolComponent, setShowNewSchoolComponent] = useState(false);

  return (
    <div className="card m-3 p-3">
      <Button
        onClick={() => {
          if (showNewSchoolComponent) setShowNewSchoolComponent(false);
          else props.defaultOnBackHandler();
        }}
        variant={"link"}
      >
        {t("back")}
      </Button>
      <Row className="text-center">
        <div className="h4">{t("registerAdministrator")}</div>
      </Row>
      <>
        {!(showNewSchoolComponent && person) ? (
          <RegisterAdministratorPerson
            onSubmit={(values: RegisterAdministratorPersonValues) => {
              setPerson(values);
              setShowNewSchoolComponent(true);
            }}
            name={person?.name}
            surname={person?.surname}
            birthday={person?.birthday}
          />
        ) : (
          <>
            <RegisterAdministratorSchool
              onSubmit={(values: RegisterAdministratorSchoolValues) => {
                setSchool(values);
                activateWithSchool(person, values);
              }}
              onContinueWithoutSchool={() => activateWithoutSchool(person)}
            />
          </>
        )}
      </>
    </div>
  );
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RegisterAdministratorForm);
