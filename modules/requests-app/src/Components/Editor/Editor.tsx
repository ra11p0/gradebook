import React from "react";
import { ListGroup, Row } from "react-bootstrap";
import EditorStage from "./EditorStage";
import EditorTitle from "./EditorTitle";
import EditorToolbar from "./EditorToolbar";

type Props = {};

function Editor({}: Props) {
  return (
    <>
      <Row>
        <EditorTitle />
      </Row>
      <Row>
        <EditorStage />
      </Row>
      <Row>
        <EditorToolbar />
      </Row>
    </>
  );
}

export default Editor;
