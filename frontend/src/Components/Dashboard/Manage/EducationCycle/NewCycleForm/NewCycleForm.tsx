import { useFormik } from "formik";
import React, { useState } from "react";
import { Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import DynamicList from "../../../../Shared/DynamicList";
import FormikInput from "../../../../Shared/FormikInput";
import NewCycleStepForm from "./NewCycleStepForm";

type Props = {};

function NewCycleForm({}: Props) {
  const { t } = useTranslation("educationCycle");
  const formik = useFormik({
    initialValues: { name: "" },
    onSubmit: (values) => {
      console.dir(values);
    },
  });
  return (
    <div>
      <form onSubmit={formik.handleSubmit}>
        <FormikInput name={"name"} formik={formik} />
        <div>
          <small>Steps</small>
          <DynamicList
            item={
              <div className="m-2 p-2 shadow border rounded-3">
                <NewCycleStepForm />
              </div>
            }
          />
        </div>
        <Button type="submit">{t("submitForm")}</Button>
      </form>
    </div>
  );
}

export default NewCycleForm;
