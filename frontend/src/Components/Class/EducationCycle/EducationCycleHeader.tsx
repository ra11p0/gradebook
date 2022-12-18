import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Tippy from '@tippyjs/react';
import moment from 'moment';
import React, { ReactElement } from 'react';
import { Button, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import Swal from 'sweetalert2';
import ClassesProxy from '../../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse from '../../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import Notifications from '../../../Notifications/Notifications';
import LoadingScreen from '../../Shared/LoadingScreen';

interface Props {
  educationCycle?: EducationCyclesInClassResponse;
  classGuid: string;
  prepareEducationCycle: () => void;
}

function EducationCycleHeader({
  educationCycle,
  classGuid,
  prepareEducationCycle,
}: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <LoadingScreen isReady={!!educationCycle}>
      <div className="d-flex gap-2 justify-content-between m-1 p-1">
        <Stack className="my-auto">
          {`${t('educationCycle')} ${
            educationCycle?.activeEducationCycle?.name
              ? ' - ' + educationCycle?.activeEducationCycle?.name
              : ''
          }`}
          {educationCycle?.activeEducationCycleInstance && (
            <small>
              {`${moment
                .utc(educationCycle.activeEducationCycleInstance.dateSince)
                .local()
                .format('ll')}
                         - 
                      ${moment
                        .utc(
                          educationCycle.activeEducationCycleInstance.dateUntil
                        )
                        .local()
                        .format('ll')}`}
            </small>
          )}
        </Stack>

        {educationCycle?.activeEducationCycle && (
          <div>
            <Tippy
              content={t('removeEducationCycleBinding')}
              arrow={true}
              animation={'scale'}
            >
              <Button
                variant="danger"
                onClick={async () => {
                  await Swal.fire({
                    showLoaderOnConfirm: true,
                    title: t('unbindEducationCycle'),
                    text: t('youSureUnbindEducationCycle'),
                    showDenyButton: true,
                    showCancelButton: false,
                    confirmButtonText: t('yes'),
                    denyButtonText: t('no'),
                    icon: 'warning',
                    preConfirm: async () => {
                      await ClassesProxy.educationCycles
                        .deleteActiveEducationCycleFromClass(classGuid)
                        .catch(Notifications.showApiError);
                    },
                  }).then(async (resp) => {
                    if (resp.isConfirmed) await prepareEducationCycle();
                  });
                }}
              >
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </Tippy>
          </div>
        )}
      </div>
    </LoadingScreen>
  );
}

export default EducationCycleHeader;
