import React, { ReactElement, useState } from 'react';
import { Stack, Table } from 'react-bootstrap';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../Notifications/Notifications';
import ClassPicker from '../Shared/ClassPicker/ClassPicker';
import InfiniteScrollWrapper from '../Shared/InfiniteScrollWrapper';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

interface Props {
  educationCycleGuid: string;
}

function EducationCycleAssignedClasses(props: Props): ReactElement {
  const { t } = useTranslation('educationCycle');
  const [refreshKey, setRefreshKey] = useState(0);
  const navigate = useNavigate();
  return (
    <Stack>
      <div>
        <div>
          <InfiniteScrollWrapper
            wrapper={(items) => (
              <Table striped bordered hover responsive>
                <thead>
                  <tr>
                    <th>{t('className')}</th>
                  </tr>
                </thead>
                <tbody>{items}</tbody>
              </Table>
            )}
            mapper={(item, index): ReactElement => (
              <tr
                className="cursor-pointer"
                onClick={() => {
                  navigate(`/class/show/${item.guid}`);
                }}
                key={index}
              >
                <td>{item.name}</td>
              </tr>
            )}
            fetch={async (page: number) => {
              const r = await EducationCyclesProxy.getClassesForEducationCycle(
                props.educationCycleGuid,
                page
              );
              return r.data;
            }}
            effect={[refreshKey]}
          />
        </div>
        <ClassPicker
          selected={async () => {
            const guids = (
              await EducationCyclesProxy.getClassesForEducationCycle(
                props.educationCycleGuid
              )
            ).data.map((e) => e.guid);
            console.dir(guids);
            return guids;
          }}
          onClassesSelected={async (guids: string[]) => {
            await EducationCyclesProxy.editClassesInEducationCycle(
              props.educationCycleGuid,
              guids
            ).catch(Notifications.showApiError);
            setRefreshKey((e) => e + 1);
          }}
        />
      </div>
    </Stack>
  );
}

export default EducationCycleAssignedClasses;
