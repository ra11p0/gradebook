import { Checkbox, Input } from '@mui/material';
import React, { ReactElement, useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import SchoolsProxy from '../../../ApiClient/Schools/SchoolsProxy';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';
import LoadingScreen from '../LoadingScreen';

interface Props {
  isModalVisible: boolean;
  onHide: () => void;
  onCyclesSelected: (selectedCyclesGuids: string[]) => void;
  selected?: () => string[] | Promise<string[]>;
  onlyOne?: boolean;
}

function EducationCyclePickerModal(props: Props): ReactElement {
  const { t } = useTranslation('educationCyclesPicker');
  const [selectedCycles, setSelectedCycles] = useState<string[] | undefined>(
    []
  );
  const [query, setQuery] = useState<string>('');
  useEffect(() => {
    setSelectedCycles([]);
    void (async () => {
      if (props.selected) {
        setSelectedCycles((await props.selected()) ?? []);
      }
    })();
  }, [props.isModalVisible]);
  return (
    <>
      <Modal show={props.isModalVisible} onHide={props.onHide}>
        <LoadingScreen isReady={selectedCycles !== undefined}>
          <>
            <Modal.Header closeButton>
              {props.onlyOne
                ? t('selectEducationCycle')
                : t('selectEducationCycles')}
            </Modal.Header>
            <Modal.Body>
              <Input
                type="text"
                className="w-100"
                placeholder={t('searchCycles')}
                value={query}
                onChange={(e) => setQuery(e.target.value)}
              />
              <div id="scrollContainer">
                <InfiniteScrollWrapper
                  scrollableTarget="scrollContainer"
                  effect={[query]}
                  mapper={(cycle, index) => (
                    <div
                      key={index}
                      className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
                      onClick={() => {
                        props.onlyOne
                          ? selectedCycles!.includes(cycle.guid)
                            ? setSelectedCycles(() => [])
                            : setSelectedCycles(() => [cycle.guid])
                          : selectedCycles!.includes(cycle.guid)
                          ? setSelectedCycles((s) =>
                              s!.filter((p) => p !== cycle.guid)
                            )
                          : setSelectedCycles((s) => [...s!, cycle.guid]);
                      }}
                    >
                      <div className="my-auto">{`${cycle.name} `}</div>
                      <div>
                        <Checkbox
                          checked={selectedCycles!.includes(cycle.guid)}
                          onChange={(e, o) =>
                            props.onlyOne
                              ? o
                                ? setSelectedCycles(() => [cycle.guid])
                                : setSelectedCycles(() => [])
                              : o
                              ? setSelectedCycles((s) => [...s!, cycle.guid])
                              : setSelectedCycles((s) =>
                                  s!.filter((p) => p !== cycle.guid)
                                )
                          }
                        />
                      </div>
                    </div>
                  )}
                  fetch={async (page: number) => {
                    return (
                      await SchoolsProxy.educationCycles.getEducationCyclesInSchool(
                        page,
                        query
                      )
                    ).data;
                  }}
                />
              </div>
            </Modal.Body>
            <Modal.Footer>
              <Button
                onClick={() => {
                  props.onCyclesSelected(selectedCycles!);
                  props.onHide();
                }}
              >
                {t('confirm')}
              </Button>
            </Modal.Footer>
          </>
        </LoadingScreen>
      </Modal>
    </>
  );
}

export default EducationCyclePickerModal;
