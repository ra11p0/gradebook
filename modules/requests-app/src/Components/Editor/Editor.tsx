import React, { useState } from "react";
import { ListGroup, Row } from "react-bootstrap";
import FieldTypes from "../../Constraints/FieldTypes";
import Field from "../../Interfaces/Common/Field";
import EditorStage from "./EditorStage";
import EditorTitle from "./EditorTitle";
import EditorToolbar from "./EditorToolbar";

type Props = {
  fields: Field[];
};

function Editor(props: Props) {
  const [fields, setFields] = useState(props.fields);
  return (
    <>
      <Row>
        <EditorTitle />
      </Row>
      <Row>
        <EditorStage
          fields={fields}
          onRemoveFieldHandler={(uuid) => {
            setFields((e) => [...e.filter((o) => o.uuid != uuid)]);
          }}
        />
      </Row>
      <Row>
        <EditorToolbar
          onAddNewFieldHandler={() => setFields((e) => [...e, { uuid: Math.random().toString(), name: "" }])}
          onConfirmHandler={() => {}}
          onDiscardHandler={() => {}}
        />
      </Row>
    </>
  );
}

export default Editor;
