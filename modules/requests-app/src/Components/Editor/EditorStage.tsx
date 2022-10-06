import React from "react";
import { ListGroup } from "react-bootstrap";
import FieldTypes from "../../Constraints/FieldTypes";
import Field from "../../Interfaces/Common/Field";
import FieldComponent from "./Fields/Field";

type Props = {
  fields: Field[];
  onRemoveFieldHandler: (uuid: string) => void;
};

function EditorStage(props: Props) {
  return (
    <div className="d-flex justify-content-center m-2 p-2">
      <ListGroup className="w-75">
        {props.fields.map((field, index) => (
          <ListGroup.Item key={index}>
            <FieldComponent field={field} onRemoveFieldHandler={() => props.onRemoveFieldHandler(field.uuid)} />
          </ListGroup.Item>
        ))}
      </ListGroup>
    </div>
  );
}

export default EditorStage;
