import { Formik } from 'formik';
import _ from 'lodash';
import React, { ReactElement, useEffect, useState } from 'react';
import { Button, Col, Row, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCycleResponse from '../../../ApiClient/EducationCycles/Definitions/Responses/EducationCycleResponse';
import EducationCyclesProxy from '../../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../../Notifications/Notifications';
import FormikInput from '../../Shared/FormikInput';
import LoadingScreen from '../../Shared/LoadingScreen';
import * as yup from 'yup';
import { TFunction } from 'i18next';
import CycleStep from './CycleStep';

interface Props {
  educationCycleGuid: string;
  onSubmit: () => void;
}

interface FormikProps {
  dateSince: Date | undefined;
  dateUntil: Date | undefined;
  stages?: Array<{
    dateSince: Date | undefined;
    dateUntil: Date | undefined;
    order: number;
  }>;
}

function datesOverlap(
  startDate1: Date,
  endDate1: Date,
  startDate2: Date,
  endDate2: Date
): boolean {
  if (startDate1 > startDate2) {
    [startDate1, endDate1, startDate2, endDate2] = [
      startDate2,
      endDate2,
      startDate1,
      endDate1,
    ];
  }
  return endDate1 > startDate2;
}

function validate(values: FormikProps, t: TFunction): any {
  const errors: any = {};
  const allCycleDateSince: Date | undefined = values.dateSince;
  const allCycleDateUntil: Date | undefined = values.dateUntil;

  //  validate inner order
  values.stages?.forEach((elem: any, index: number) => {
    const elementDateSince: Date | undefined = elem.dateSince;
    const elementDateUntil: Date | undefined = elem.dateUntil;

    if (elementDateSince && elementDateUntil) {
      if (allCycleDateSince && allCycleDateUntil) {
        if (elementDateSince < allCycleDateSince)
          _.set(
            errors,
            `stages.${index}.dateSince`,
            t('shouldBeAfterCycleDateSince')
          );
        if (elementDateUntil > allCycleDateUntil)
          _.set(
            errors,
            `stages.${index}.dateUntil`,
            t('shouldBeBeforeCycleDateUntil')
          );
      }
      if (elementDateSince > elementDateUntil)
        _.set(
          errors,
          `stages.${index}.dateSince`,
          t('shouldBeBeforeDateUntil')
        );
    } else if (elementDateSince ?? elementDateUntil) {
      _.set(errors, `stages.${index}.dateSince`, t('fillBothDateValues'));
      _.set(errors, `stages.${index}.dateUntil`, t('fillBothDateValues'));
    }
  });

  const stagesWithBothDates = values.stages!.filter(
    (e: any) => e.dateSince && e.dateUntil
  );

  //  validate overlaping dates
  stagesWithBothDates.forEach((element, index: number) => {
    stagesWithBothDates.forEach((element2, index2) => {
      if (element.order === element2.order) return;
      if (
        datesOverlap(
          element.dateSince!,
          element.dateUntil!,
          element2.dateSince!,
          element2.dateUntil!
        )
      ) {
        _.set(errors, `stages.${index}.dateSince`, t('datesShouldNotOverlap'));
        _.set(errors, `stages.${index}.dateSince`, t('datesShouldNotOverlap'));
      }
    });
  });

  let dateThatShouldNotBeLater: Date | undefined;
  //  validate outer order
  stagesWithBothDates
    .sort((a, b) => a.order - b.order)
    .forEach((el) => {
      if (dateThatShouldNotBeLater && dateThatShouldNotBeLater > el.dateSince!)
        _.set(errors, `stages.${el.order}.dateSince`, t('wrongDatesOrder'));
      dateThatShouldNotBeLater = el.dateSince!;
    });

  return errors;
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
          validationSchema={yup.object().shape({
            dateSince: yup.date().required(t('fieldRequired')),
            dateUntil: yup
              .date()
              .required(t('fieldRequired'))
              .when('dateSince', (dateSince) => {
                if (dateSince)
                  return yup
                    .date()
                    .required(t('fieldRequired'))
                    .min(dateSince, t('shouldNotBeBeforeCycleDateSince'));
                return yup.date().required(t('fieldRequired'));
              }),
            stages: yup.array().of(
              yup.object().shape({
                datesince: yup.date(),
                dateUntil: yup.date(),
                subjects: yup.array().of(
                  yup.object().shape({
                    teacher: yup.object().required(t('fieldRequired')),
                  })
                ),
              })
            ),
          })}
          validate={(vals) => validate(vals, t)}
          initialValues={{
            ...educationCycle,
            dateSince: undefined as Date | undefined,
            dateUntil: undefined as Date | undefined,
            stages: educationCycle?.stages.map((stage) => ({
              ...stage,
              dateSince: undefined as Date | undefined,
              dateUntil: undefined as Date | undefined,
              subjects: stage.subjects.map((subject) => ({
                ...subject,
                teacher: undefined,
              })),
            })),
          }}
          onSubmit={(values) => {
            console.dir(values);
          }}
        >
          {(formik) => (
            <form onSubmit={formik.handleSubmit}>
              <Stack>
                <h5>{formik.values.name}</h5>
                <Row>
                  <Col md={6} s={12}>
                    <FormikInput
                      type="date"
                      name="dateSince"
                      label={t('dateSince')}
                      formik={formik}
                    />
                  </Col>
                  <Col md={6} s={12}>
                    <FormikInput
                      type="date"
                      name="dateUntil"
                      label={t('dateUntil')}
                      formik={formik}
                    />
                  </Col>
                </Row>

                <h6>{t('educationCyclesSteps')}</h6>
                {formik.values.stages
                  ?.sort((a, b) => a.order - b.order)
                  .map((stage, key) => (
                    <CycleStep
                      key={key}
                      {...{ stage, formik, stageKey: key }}
                    />
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
