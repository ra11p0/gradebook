import { List, ListItem, Table, TableBody, TableCell, TableContainer, TableRow } from "@mui/material";
import moment from "moment";
import React from "react";
import { TFunction, withTranslation } from "react-i18next";
import { connect } from "react-redux";
import AccountProxy from "../../ApiClient/Accounts/AccountsProxy";
import MeResponse from "../../ApiClient/Accounts/Definitions/Responses/MeResponse";
import { isLoggedInProxy } from "../../Redux/ReduxProxy/isLoggedInProxy";
import { logOutWrapper } from "../../Redux/ReduxWrappers/logOutWrapper";

const mapStateToProps = (state: any) => {
  return {
    isLoggedIn: isLoggedInProxy(state),
    currentSchoolGuid: state.common.school?.schoolGuid,
  };
};

const mapDispatchToProps = (dispatch: any) => ({
  logOutHandler: () => logOutWrapper(dispatch),
});

interface ProfileProps {
  isLoggedIn?: boolean;
  logOutHandler?: () => void;
  t: TFunction<"translation", any>;
  currentSchoolGuid?: string;
}

interface ProfileState {
  isLoggedIn?: boolean;
  weather?: any;
  me: MeResponse | null;
}

class Profile extends React.Component<ProfileProps, ProfileState> {
  constructor(props: ProfileProps) {
    super(props);
    this.state = {
      isLoggedIn: props.isLoggedIn,
      me: null,
    };
  }
  async componentDidMount() {
    AccountProxy.getMe().then((response) => this.setState({ ...this.state, me: response.data }));
  }
  render(): React.ReactNode {
    const t = this.props.t;
    const me = this.state.me;
    return (
      <div>
        <TableContainer>
          {/* <Table>
            <TableBody>
              <TableRow>
                <TableCell>{t("id")}</TableCell>
                <TableCell>{me?.id}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("userName")}</TableCell>
                <TableCell>{me?.username}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("name")}</TableCell>
                <TableCell>{me?.name}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("surname")}</TableCell>
                <TableCell>{me?.surname}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("birthday")}</TableCell>
                <TableCell>{moment(me?.birthday).format("YYYY-MM-DD")}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("personGuid")}</TableCell>
                <TableCell>{me?.personGuid}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("roles")}</TableCell>
                <TableCell>
                  <List>
                    {me?.roles?.map((element, index) => (
                      <ListItem key={index}>{element}</ListItem>
                    ))}
                  </List>
                </TableCell>
              </TableRow>
              <TableRow>
                <TableCell>{t("schoolRole")}</TableCell>
                <TableCell>{me?.schoolRole}</TableCell>
              </TableRow>
            </TableBody>
          </Table> */}
        </TableContainer>
      </div>
    );
  }
}

export default withTranslation("profile")(connect(mapStateToProps, mapDispatchToProps)(Profile));
