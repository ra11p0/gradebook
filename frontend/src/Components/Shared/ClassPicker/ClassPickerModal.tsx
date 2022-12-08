import { Checkbox, Input } from '@mui/material';
import React, { ReactElement, useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import SchoolsProxy from '../../../ApiClient/Schools/SchoolsProxy';
import getCurrentSchoolRedux from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';

interface Props {
  isModalVisible: boolean;
  onHide: () => void;
  onClassesSelected: (selectedClassesGuids: string[]) => void;
  selected?: string[] | Promise<string[]>;
}

function ClassPickerModal(props: Props): ReactElement {
  const { t } = useTranslation('classPicker');
  const [selectedClasses, setSelectedClasses] = useState<string[]>([]);
  const [query, setQuery] = useState<string>('');
  useEffect(() => {
    void (async () => {
      const selected = await props.selected;
      setSelectedClasses(selected ?? []);
    })();
  }, []);
  return (
    <Modal show={props.isModalVisible} onHide={props.onHide}>
      <Modal.Header closeButton>{t('selectClasses')}</Modal.Header>
      <Modal.Body>
        <Input
          type="text"
          className="w-100"
          placeholder={t('searchClasses')}
          value={query}
          onChange={(e) => setQuery(e.target.value)}
        />
        <div id="scrollContainer">
          <InfiniteScrollWrapper
            scrollableTarget="scrollContainer"
            effect={[query]}
            mapper={(_class, index) => (
              <div
                key={index}
                className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
                onClick={() => {
                  selectedClasses.includes(_class.guid)
                    ? setSelectedClasses((s) =>
                        s.filter((p) => p !== _class.guid)
                      )
                    : setSelectedClasses((s) => [...s, _class.guid]);
                }}
              >
                <div className="my-auto">{`${_class.name} `}</div>
                <div>
                  <Checkbox
                    checked={selectedClasses.includes(_class.guid)}
                    onChange={(e, o) =>
                      o
                        ? setSelectedClasses((s) => [...s, _class.guid])
                        : setSelectedClasses((s) =>
                            s.filter((p) => p !== _class.guid)
                          )
                    }
                  />
                </div>
              </div>
            )}
            fetch={async (page: number) => {
              return (
                await SchoolsProxy.getClassesInSchool(
                  getCurrentSchoolRedux()?.schoolGuid,
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
            props.onClassesSelected(selectedClasses);
          }}
        >
          {t('confirm')}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default ClassPickerModal;
