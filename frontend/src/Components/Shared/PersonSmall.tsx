import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Badge } from "react-bootstrap";
const mapStateToProps = (state: any) => ({
  isSuperAdmin: state.common.session.roles.includes("SuperAdmin"),
});
const mapDispatchToProps = (dispatch: any) => ({});
interface PersonProps {
  name: string;
  surname: string;
  className?: string;
}
const PersonSmall = (props: PersonProps): ReactElement => {
  const { t } = useTranslation("person");
  return (
    <Badge className={`m-1 p-2 ${props.className}`}>
      <span>{`${props.name} ${props.surname}`}</span>
    </Badge>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(PersonSmall);
