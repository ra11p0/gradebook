import React from "react";
import { Button, ButtonGroup, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import FieldTypes from "../../../Constraints/FieldTypes";

type Props = {
  field: FieldValues;
  onEditFieldHandler: () => void;
  onRemoveFieldHandler: () => void;
};

type FieldValues = {
  name: string;
  type?: FieldTypes;
  value?: any;
};

function FieldPreview(props: Props) {
  const { t } = useTranslation();
  return (
    <div>
      <Row>{props.field.name as string}</Row>
      <Row>
        <div className="d-flex justify-content-end">
          <ButtonGroup>
            <Button variant="danger" onClick={props.onRemoveFieldHandler}>
              {t("removeField")}
            </Button>
            <Button onClick={props.onEditFieldHandler}>{t("editField")}</Button>
          </ButtonGroup>
        </div>
      </Row>
    </div>
  );
}

export default FieldPreview;
