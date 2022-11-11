import React from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import AddNewStudentModal from "./AddNewStudentModal";

type Props = {
  setShowAddStudentModal: (visible: boolean) => void;
  showAddStudentModal: boolean;
};

function AddNewStudentModalWithButton(props: Props) {
  const { t } = useTranslation("studentsList");
  return (
    <>
      <AddNewStudentModal
        show={props.showAddStudentModal}
        onHide={() => {
          props.setShowAddStudentModal(false);
        }}
      />
      <Button onClick={() => props.setShowAddStudentModal(true)} className="addNewStudentButton">
        {t("addStudent")}
      </Button>
    </>
  );
}

export default AddNewStudentModalWithButton;
