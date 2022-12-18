import { faForward, faPlay, faStop } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Tippy from '@tippyjs/react';
import moment from 'moment';
import React, { ReactElement } from 'react';
import { Button, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ClassesProxy from '../../../ApiClient/Classes/ClassesProxy';
import { EducationCycleStepInstance } from '../../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import Notifications from '../../../Notifications/Notifications';

function EducationCycleStepInstanceCurrent(
  props: EducationCycleStepInstance & {
    isLast: boolean;
    stateChanged: () => void;
    classGuid: string;
  }
): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <>
      <Stack className="border-bottom border-top py-1">
        <div>
          {`${props.educationCycleStepName}${
            props.dateSince && props.dateUntil
              ? ` (${moment.utc(props.dateSince).local().format('ll')}
           - 
          ${moment.utc(props.dateUntil).local().format('ll')})`
              : ''
          }`}
        </div>
        <Stack className="gap-1">
          <small>
            {props.started && <>{t('educationCycleStartedDescription')}</>}
            {!props.isLast && props.started && (
              <>{t('educationCycleStartedNotLastDescription')}</>
            )}
            {!props.started && <>{t('educationCycleNotStartedDescription')}</>}
            {props.started && props.isLast && (
              <>{t('educationCycleStartedLastDescription')}</>
            )}
          </small>

          {!props.isLast && props.started && (
            <Tippy
              content={t('educationCycleForward')}
              arrow={true}
              animation={'scale'}
            >
              <Button
                variant="success"
                onClick={async () => {
                  await ClassesProxy.educationCycles
                    .forwardEducationCycleStepInstance(props.classGuid)
                    .catch(Notifications.showApiError);
                  props.stateChanged();
                }}
              >
                <FontAwesomeIcon icon={faForward} />
              </Button>
            </Tippy>
          )}
          {!props.started && (
            <Tippy
              content={t('educationCycleStart')}
              arrow={true}
              animation={'scale'}
            >
              <Button
                variant="primary"
                onClick={async () => {
                  await ClassesProxy.educationCycles
                    .startEducationCycleStepInstance(props.classGuid)
                    .catch(Notifications.showApiError);
                  props.stateChanged();
                }}
              >
                <FontAwesomeIcon icon={faPlay} />
              </Button>
            </Tippy>
          )}

          {props.isLast && (
            <Tippy
              content={t('educationCycleStop')}
              arrow={true}
              animation={'scale'}
            >
              <Button
                variant="danger"
                onClick={async () => {
                  await ClassesProxy.educationCycles
                    .stopEducationCycleStepInstance(props.classGuid)
                    .catch(Notifications.showApiError);
                  props.stateChanged();
                }}
              >
                <FontAwesomeIcon icon={faStop} />
              </Button>
            </Tippy>
          )}
        </Stack>
      </Stack>
    </>
  );
}

export default EducationCycleStepInstanceCurrent;
