import React, { ReactElement, useEffect, useState } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Button, Row } from "react-bootstrap";
import AdministratorsProxy from "../../../ApiClient/Administrators/AdministratorsProxy";
import Notifications from "../../../Notifications/Notifications";
import AccountProxy from "../../../ApiClient/Accounts/AccountsProxy";
import getCurrentUserIdReduxProxy from "../../../Redux/ReduxProxy/getCurrentUserIdReduxProxy";
import setSchoolsListReduxWrapper, { setSchoolsListAction } from "../../../Redux/ReduxWrappers/setSchoolsListReduxWrapper";
import ActivateAdministratorPerson, { ActivateAdministratorPersonValues } from "./ActivateAdministratorPerson";
import ActivateAdministratorSchool, { ActivateAdministratorSchoolValues } from "./ActivateAdministratorSchool";

const mapStateToProps = (state: any) => ({
  userId: getCurrentUserIdReduxProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolsList: (action: setSchoolsListAction) => setSchoolsListReduxWrapper(dispatch, action),
});

interface ActivateAdministratorFormProps {
  defaultOnBackHandler: () => void;
  userId: string;
  setSchoolsList: (action: setSchoolsListAction) => void;
  onSubmit?: () => void;
  person?: ActivateAdministratorPersonValues;
}

const ActivateAdministratorForm = (props: ActivateAdministratorFormProps): ReactElement => {
  const { t } = useTranslation("ActivateAdministrator");
  const [person, setPerson] = useState<ActivateAdministratorPersonValues | null>(null);
  const [showNewSchoolComponent, setShowNewSchoolComponent] = useState(false);

  useEffect(() => {
    if (props.person) {
      setPerson(props.person);
      setShowNewSchoolComponent(true);
    }
  }, []);

  const activateWithSchool = (person: ActivateAdministratorPersonValues, school: ActivateAdministratorSchoolValues) => {
    AdministratorsProxy.newAdministratorWithSchool({ ...person, birthday: new Date(person.birthday) }, school).then((response) => {
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
        <div className="h4">{t("ActivateAdministrator")}</div>
      </Row>
      <>
        {!(showNewSchoolComponent && person) ? (
          <ActivateAdministratorPerson
            onSubmit={(values: ActivateAdministratorPersonValues) => {
              setPerson(values);
              setShowNewSchoolComponent(true);
            }}
            name={person?.name}
            surname={person?.surname}
            birthday={person?.birthday}
          />
        ) : (
          <>
            <ActivateAdministratorSchool
              onSubmit={(values: ActivateAdministratorSchoolValues) => {
                activateWithSchool(person, values);
              }}
            />
          </>
        )}
      </>
    </div>
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(ActivateAdministratorForm);
