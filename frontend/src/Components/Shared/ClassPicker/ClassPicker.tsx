import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ClassResponse from '../../../ApiClient/Schools/Definitions/Responses/ClassResponse';
import ClassPickerModal from './ClassPickerModal';

interface Props {
  onClassesSelected?: (guids: string[]) => void;
  selected?: () => string[] | Promise<string[]>;
  fetch?: (page: number, query: string) => Promise<ClassResponse[]>;
}

function ClassPicker(props: Props): ReactElement {
  const { t } = useTranslation('classPicker');
  const [isModalVisible, setIsModalVisible] = useState(false);

  return (
    <>
      <ClassPickerModal
        fetch={props.fetch}
        isModalVisible={isModalVisible}
        selected={props.selected}
        onHide={() => {
          setIsModalVisible(false);
        }}
        onClassesSelected={(guids) => {
          if (props.onClassesSelected) props.onClassesSelected(guids);
        }}
      />
      <Button
        onClick={() => {
          setIsModalVisible(true);
        }}
      >
        {t('selectClasses')}
      </Button>
    </>
  );
}

export default ClassPicker;
