import { ListGroup } from "react-bootstrap";
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
    <div className="d-flex justify-content-center m-2 p-2">
      <DndProvider backend={HTML5Backend}>
        <ListGroup className="w-75">
          {props.fields.map((field, index) => (
            <ListGroup.Item key={index}>
              <FieldComponent field={field} onRemoveFieldHandler={() => props.onRemoveFieldHandler(field.uuid)} />
            </ListGroup.Item>
          ))}
        </ListGroup>
      </DndProvider>
    </div>
  );
}

export default EditorStage;
