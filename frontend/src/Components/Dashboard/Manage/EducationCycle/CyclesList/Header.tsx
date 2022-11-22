import React from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router";

type Props = {};

function Header({ }: Props) {
  const { t } = useTranslation("educationCycle");
  const navigate = useNavigate();
  return (
    <div className="d-flex justify-content-between">
      <div>
        <h5>{t('educationCycle')}</h5>
      </div>
      <div>
        <Button
          onClick={() => {
            navigate(`new`);
          }}
        >
          {t("addNewEducationCycle")}
        </Button>
      </div>
    </div>
  );
}

export default Header;
