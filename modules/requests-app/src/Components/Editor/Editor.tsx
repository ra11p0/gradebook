import React, { useState } from "react";
import { Col, ListGroup, Row } from "react-bootstrap";
import { connect } from "react-redux";
import FieldTypes from "../../Constraints/FieldTypes";
import Field from "../../Interfaces/Common/Field";
import ReduxGetFields from "../../Redux/ReduxGet/ReduxGetFields";
import ReduxFixFields from "../../Redux/ReduxSet/ReduxFixFields";
import ReduxRemoveField from "../../Redux/ReduxSet/ReduxRemoveField";
import ReduxSetCurrentlyEdited from "../../Redux/ReduxSet/ReduxSetCurrentlyEdited";
import ReduxSetField from "../../Redux/ReduxSet/ReduxSetField";
import ReduxSetFields from "../../Redux/ReduxSet/ReduxSetFields";
import EditorStage from "./EditorStage";
import EditorTitle from "./EditorTitle";
import EditorToolbar from "./EditorToolbar";

type Props = {
  fields: Field[];
};

/* const fields: Field[] = [
  {
    uuid: "aaaaa",
    name: "Imię i nazwisko",
    type: FieldTypes.ShortText,
    value: "Marian broda",
  },
  {
    uuid: "bbbbb",
    name: "Odpowiedz na pytanie",
    type: FieldTypes.LongText,
    value: "To jest bardzo dluga odpowiedz na pytanie w tym momencie prosze bardzo.",
  },
]; */

function Editor(props: Props) {
  return (
    <>
      <Row>
        <Col>
          <EditorTitle />
        </Col>
      </Row>
      <Row>
        <Col>
          <EditorStage
            fields={props.fields}
            onRemoveFieldHandler={(uuid) => {
              let fieldToRemove = props.fields.find((o) => o.uuid == uuid);
              if (!fieldToRemove) return;
              ReduxRemoveField(fieldToRemove.uuid);
            }}
          />
        </Col>
      </Row>
      <Row>
        <Col>
          <EditorToolbar
            onAddNewFieldHandler={() => {
              ReduxFixFields();
              const newUuid = Math.random().toString();
              ReduxSetField({ uuid: newUuid, name: "" });
              ReduxSetCurrentlyEdited(newUuid);
            }}
            onConfirmHandler={() => { }}
            onDiscardHandler={() => { }}
          />
        </Col>
      </Row>
    </>
  );
}

export default connect(
  (state) => ({
    fields: ReduxGetFields(state),
  }),
  () => ({})
)(Editor);
