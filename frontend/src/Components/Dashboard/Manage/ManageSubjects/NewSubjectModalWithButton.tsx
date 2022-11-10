import React, { useState } from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import NewSubjectModal from "./NewSubjectModal";

type Props = {};

function NewSubjectModalWithButton({}: Props) {
  const [showNewSubjectModal, setShowNewSubjectModal] = useState(false);
  const { t } = useTranslation();
  return (
    <div>
      <Button
        onClick={() => {
          setShowNewSubjectModal(true);
        }}
      >
        {t("newSubject")}
      </Button>
      <NewSubjectModal
        show={showNewSubjectModal}
        onHide={() => {
          setShowNewSubjectModal(false);
        }}
      />
    </div>
  );
}

export default NewSubjectModalWithButton;
