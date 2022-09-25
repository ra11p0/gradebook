import React from "react";
import { Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import ActivateAccount from "../../../Account/ActivateAccount";

interface JoinSchoolModalProps {
  show: boolean;
  onHide: () => void;
}
function JoinSchoolModal(props: JoinSchoolModalProps) {
  const { t } = useTranslation("registerAdministratorSchool");
  return (
    <Modal show={props.show} onHide={props.onHide} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>{t("joinSchool")}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <ActivateAccount onSubmit={props.onHide} />
      </Modal.Body>
    </Modal>
  );
}

export default JoinSchoolModal;
