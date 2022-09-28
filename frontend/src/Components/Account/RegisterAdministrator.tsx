import React, { ReactElement, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button, Row } from "react-bootstrap";
import RegisterAdministratorPerson, { RegisterAdministratorPersonValues } from "./RegisterAdministratorPerson";
import RegisterAdministratorSchool, { RegisterAdministratorSchoolValues } from "./RegisterAdministratorSchool";
import AdministratorsProxy from "../../ApiClient/Administrators/AdministratorsProxy";
import Notifications from "../../Notifications/Notifications";
import AccountProxy from "../../ApiClient/Accounts/AccountsProxy";
import { currentUserIdProxy } from "../../Redux/ReduxProxy/getCurrentUserIdReduxProxy";
import { setSchoolsListAction, setSchoolsListWrapper } from "../../Redux/ReduxWrappers/setSchoolsListReduxWrapper";

const mapStateToProps = (state: any) => ({
  userId: currentUserIdProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolsList: (action: setSchoolsListAction) => setSchoolsListWrapper(dispatch, action),
});

interface RegisterAdministratorFormProps {
  defaultOnBackHandler: () => void;
  userId: string;
  setSchoolsList: (action: setSchoolsListAction) => void;
  onSubmit?: () => void;
}

const RegisterAdministratorForm = (props: RegisterAdministratorFormProps): ReactElement => {
  const { t } = useTranslation("registerAdministrator");
  const [person, setPerson] = useState<RegisterAdministratorPersonValues | null>(null);
  const [showNewSchoolComponent, setShowNewSchoolComponent] = useState(false);

  const activateWithSchool = (person: RegisterAdministratorPersonValues, school: RegisterAdministratorSchoolValues) => {
    AdministratorsProxy.newAdministratorWithSchool(person, school).then((response) => {
      AccountProxy.getAccessibleSchools(props.userId!)
        .then((schoolsResponse) => {
          props.setSchoolsList!({ schoolsList: schoolsResponse.data });
          if (props.onSubmit) props.onSubmit();
        })
        .catch(Notifications.showApiError);
    });
  };

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
                activateWithSchool(person, values);
              }}
            />
          </>
        )}
      </>
    </div>
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(RegisterAdministratorForm);
