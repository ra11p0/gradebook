import { Button, ButtonGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";

type Props = {
  onAddNewFieldHandler: () => void;
  onDiscardHandler: () => void;
  onConfirmHandler: () => void;
};

function EditorToolbar(props: Props) {
  const { t } = useTranslation();
  return (
    <div className="d-flex justify-content-center m-2 p-2">
      <ButtonGroup>
        <Button onClick={props.onAddNewFieldHandler}>{t("addNewField")}</Button>
        <Button onClick={props.onDiscardHandler} variant="danger">
          {t("discard")}
        </Button>
        <Button onClick={props.onConfirmHandler} variant="success">
          {t("confirm")}
        </Button>
      </ButtonGroup>
    </div>
  );
}

export default EditorToolbar;
