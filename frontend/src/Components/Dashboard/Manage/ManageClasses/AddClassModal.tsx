import React from "react";
import { Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";

type Props = {
  show: boolean;
  onHide: () => void;
};

function AddClassModal(props: Props) {
  const { t } = useTranslation("addClassModal");
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t("addClass")}</Modal.Title>
      </Modal.Header>
      <Modal.Body></Modal.Body>
      <Modal.Footer></Modal.Footer>
    </Modal>
  );
}

export default AddClassModal;
