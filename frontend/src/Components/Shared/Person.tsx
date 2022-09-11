import React, { ReactElement } from "react";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import { Col, Row } from "react-bootstrap";
import moment from "moment";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface PersonProps {
  guid: string;
  name: string;
  surname: string;
  birthday: Date;
  className?: string;
}
const Person = (props: PersonProps): ReactElement => {
  const { t } = useTranslation("person");
  return (
    <Link to={`/person/show/${props.guid}`} className={"text-reset"}>
      <div className={`bg-light border rounded-3 m-1 p-2 ${props.className}`}>
        <Row>
          <Col xs={2} className="my-auto text-center">
            <FontAwesomeIcon icon={faUser} />
          </Col>
          <Col>
            <Row>{props.name}</Row>
            <Row>{props.surname}</Row>
          </Col>
          <Col className="my-auto">
            {moment(props.birthday).format("YYYY-MM-DD")}
          </Col>
        </Row>
      </div>
    </Link>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(Person);
