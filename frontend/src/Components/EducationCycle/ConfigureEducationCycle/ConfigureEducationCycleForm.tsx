import { Formik } from 'formik';
import React, { ReactElement, useEffect, useState } from 'react';
import { Button, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCycleResponse from '../../../ApiClient/EducationCycles/Definitions/Responses/EducationCycleResponse';
import EducationCyclesProxy from '../../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../../Notifications/Notifications';
import FormikInput from '../../Shared/FormikInput';
import LoadingScreen from '../../Shared/LoadingScreen';

interface Props {
  educationCycleGuid: string;
  onSubmit: () => void;
}

function ConfigureEducationCycleForm(props: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  const [educationCycle, setEducationCycle] = useState<
    EducationCycleResponse | undefined
  >(undefined);

  useEffect(() => {
    void (async () => {
      await EducationCyclesProxy.getEducationCycle(props.educationCycleGuid)
        .then((resp) => {
          setEducationCycle(resp.data);
        })
        .catch(Notifications.showApiError);
    })();
  }, []);

  return (
    <>
      <LoadingScreen isReady={!!educationCycle}>
        <Formik
          initialValues={{ ...educationCycle }}
          onSubmit={(values) => {
            console.dir(values);
          }}
        >
          {(formik) => (
            <form onSubmit={formik.handleSubmit}>
              <Stack>
                <FormikInput type="date" name="dateSince" formik={formik} />
                <FormikInput type="date" name="dateUntil" formik={formik} />
                <small>{t('educationCyclesSteps')}</small>
                {formik.values.stages
                  ?.sort((a, b) => a.order - b.order)
                  .map((stage, key) => (
                    <Stack key={key}>
                      <small>{`${stage.order + 1}.${stage.name}`}</small>
                      <FormikInput
                        name={`stages.${key}.someText`}
                        formik={formik}
                      />
                    </Stack>
                  ))}
              </Stack>
              <Button type="submit"> submit</Button>
            </form>
          )}
        </Formik>
      </LoadingScreen>
    </>
  );
}

export default ConfigureEducationCycleForm;
