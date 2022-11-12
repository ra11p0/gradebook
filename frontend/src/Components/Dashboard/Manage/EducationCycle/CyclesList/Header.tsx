import React from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router";

type Props = {};

function Header({}: Props) {
  const { t } = useTranslation("educationCycle");
  const navigate = useNavigate();
  return (
    <div className="d-flex justify-content-end">
      <Button
        onClick={() => {
          navigate(`new`);
        }}
      >
        {t("addNew")}
      </Button>
    </div>
  );
}

export default Header;
