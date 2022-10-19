import { faHandDots } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, ButtonGroup, Col, Form, Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import FieldTypes from "../../../Constraints/FieldTypes";
import Field from "../../../Interfaces/Common/Field";
import Checkbox from "./FieldTypes/Checkbox";
import Date from "./FieldTypes/Date";
import Email from "./FieldTypes/Email";
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
    <>
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
          {
            props.field.description &&
            <Row>
              <Col>

                <Form.Text>
                  {props.field.description}
                </Form.Text>

              </Col>
            </Row>
          }
          {(() => {
            switch (props.field.type) {
              case FieldTypes.ShortText:
                return <ShortText {...props.field} />;
              case FieldTypes.LongText:
                return <LongText {...props.field} />;
              case FieldTypes.Checkbox:
                return <Checkbox {...props.field} />;
              case FieldTypes.Date:
                return <Date {...props.field} />;
              case FieldTypes.Email:
                return <Email {...props.field} />;
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
