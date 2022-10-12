import { faHandDots } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import FieldTypes from "../../../Constraints/FieldTypes";
import Field from "../../../Interfaces/Common/Field";
import Checkbox from "./FieldTypes/Checkbox";
import LongText from "./FieldTypes/LongText";
import ShortText from "./FieldTypes/ShortText";

type Props = {
  field: Field;
  onEditFieldHandler: () => void;
  onRemoveFieldHandler: () => void;
  isHovered?: boolean;
};

function FieldPreview(props: Props) {
  const { t } = useTranslation();
  return (
    < >
      <Row className={((props.isHovered ?? false) ? '' : 'invisible') + ' position-static'}>
        <Col className="d-flex justify-content-between">
          <FontAwesomeIcon icon={faHandDots} />
          <ButtonGroup>
            <Button variant="danger" onClick={props.onRemoveFieldHandler}>
              {t("removeField")}
            </Button>
            <Button onClick={props.onEditFieldHandler}>{t("editField")}</Button>
          </ButtonGroup>
        </Col>
      </Row>
      <Row>
        <Col>
          <Row>
            <Col>
              <Form.Label>
                {props.field.name}
              </Form.Label>
            </Col>
          </Row>
          <Row>
            <Col>          {
              props.field.description && props.field.description != '' &&
              <Form.Text>
                {props.field.description}
              </Form.Text>
            }
            </Col>
          </Row>
          {(() => {
            switch (props.field.type) {
              case FieldTypes.ShortText:
                return <ShortText value={props.field.value} />;
              case FieldTypes.LongText:
                return <LongText value={props.field.value} />;
              case FieldTypes.Checkbox:
                return <Checkbox value={props.field.value} labels={props.field.labels ?? []} distinctValues={props.field.distinctValues} />;
              default:
                return null;
            }
          })()}
        </Col>
      </Row>
    </>
  );
}

export default FieldPreview;
