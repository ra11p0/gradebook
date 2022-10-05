import React from "react";
import { Button, ButtonGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";

type Props = {};

function EditorToolbar({}: Props) {
  const { t } = useTranslation();
  return (
    <div className="d-flex justify-content-center m-2 p-2">
      <ButtonGroup>
        <Button>{t("addNewField")}</Button>
        <Button variant="danger">{t("discard")}</Button>
        <Button variant="success">{t("confirm")}</Button>
      </ButtonGroup>
    </div>
  );
}

export default EditorToolbar;
