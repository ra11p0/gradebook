import { Col, Row } from "react-bootstrap";
import { DndProvider } from "react-dnd";
import Field from "../../Interfaces/Common/Field";
import FieldComponent from "./Fields/Field";
import { HTML5Backend } from "react-dnd-html5-backend";

type Props = {
  fields: Field[];
  onRemoveFieldHandler: (uuid: string) => void;
};

function EditorStage(props: Props) {
  return (
    <DndProvider backend={HTML5Backend}>
      <Row className="d-flex justify-content-center">
        <Col>
          {props.fields.map((field, index) => (
            <FieldComponent key={index} field={field} onRemoveFieldHandler={() => props.onRemoveFieldHandler(field.uuid)} />
          ))}
        </Col>
      </Row>
    </DndProvider >
  );
}

export default EditorStage;
