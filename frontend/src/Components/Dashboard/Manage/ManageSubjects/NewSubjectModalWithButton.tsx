import React, { useState } from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import NewSubjectModal from "./NewSubjectModal";

type Props = {
  onHide: () => void;
};

function NewSubjectModalWithButton(props: Props) {
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
          props.onHide();
        }}
      />
    </div>
  );
}

export default NewSubjectModalWithButton;
