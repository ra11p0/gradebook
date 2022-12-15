import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCyclePickerModal from './EducationCyclePickerModal';

interface Props {
  onCyclesSelected?: (guids: string[]) => void;
  selected?: () => string[] | Promise<string[]>;
  onlyOne?: boolean;
}

function EducationCyclePicker(props: Props): ReactElement {
  const { t } = useTranslation('educationCyclesPicker');
  const [isModalVisible, setIsModalVisible] = useState(false);
  return (
    <>
      <EducationCyclePickerModal
        isModalVisible={isModalVisible}
        selected={props.selected}
        onHide={() => {
          setIsModalVisible(false);
        }}
        onCyclesSelected={(guids) => {
          if (props.onCyclesSelected) props.onCyclesSelected(guids);
        }}
        onlyOne={props.onlyOne}
      />
      <Button
        onClick={() => {
          setIsModalVisible(true);
        }}
      >
        {props.onlyOne ? t('selectEducationCycle') : t('selectEducationCycles')}
      </Button>
    </>
  );
}

export default EducationCyclePicker;
