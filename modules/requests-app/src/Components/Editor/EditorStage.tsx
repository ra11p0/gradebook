import React from "react";
import { ListGroup } from "react-bootstrap";
import { DndProvider } from "react-dnd";
import FieldTypes from "../../Constraints/FieldTypes";
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
      <ListGroup className="w-75">
        <DndProvider backend={HTML5Backend}>
          {props.fields.map((field, index) => (
            <FieldComponent key={index} field={field} onRemoveFieldHandler={() => props.onRemoveFieldHandler(field.uuid)} />
          ))}
        </DndProvider>
      </ListGroup>
    </div>
  );
}

export default EditorStage;
