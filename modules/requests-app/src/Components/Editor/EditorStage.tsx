import React from "react";
import { ListGroup } from "react-bootstrap";
import FieldTypes from "../../Constraints/FieldTypes";
import Field from "./Fields/Field";

type Props = {};

function EditorStage({}: Props) {
  return (
    <div className="d-flex justify-content-center m-2 p-2">
      <ListGroup>
        <ListGroup.Item>
          <Field
            field={{
              name: "Imię i nazwisko",
              type: FieldTypes.ShortText,
              value: "Marian broda",
            }}
          ></Field>
        </ListGroup.Item>
      </ListGroup>
    </div>
  );
}

export default EditorStage;
