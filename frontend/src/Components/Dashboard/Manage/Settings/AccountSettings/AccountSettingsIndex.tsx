import { Button } from "@mui/material";
import React from "react";
import { ListGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import AccountProxy from "../../../../../ApiClient/Accounts/AccountsProxy";
import DefaultPersonSettingElement from "./SettingsElements/DefaultPersonSettingElement";
import { connect } from "react-redux";
import getCurrentUserIdReduxProxy from "../../../../../Redux/ReduxQueries/getCurrentUserIdRedux";

const mapStateToProps = (state: any) => ({
  currentUserGuid: getCurrentUserIdReduxProxy(state),
});

type Props = { currentUserGuid?: string };

function AccountSettingsIndex(props: Props) {
  const { t } = useTranslation("settings");
  const settings: any = {};
  return (
    <>
      <ListGroup>
        <ListGroup.Item>
          <DefaultPersonSettingElement onChange={(e) => (settings.defaultPersonGuid = e)} />
        </ListGroup.Item>
      </ListGroup>
      <div className="d-flex justify-content-end m-2 p-2">
        <Button
          className="saveSettingsButton"
          variant="outlined"
          onClick={() => {
            if (!props.currentUserGuid) return;
            AccountProxy.settings.setSettings(props.currentUserGuid!, settings);
            console.dir(settings);
          }}
        >
          {t("save")}
        </Button>
      </div>
    </>
  );
}

export default connect(mapStateToProps, () => ({}))(AccountSettingsIndex);
