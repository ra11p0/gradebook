import React, { ReactElement, useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import getCurrentSchoolRedux from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import ClassSelect from './ClassSelect';
import RoleSelect from './RoleSelect';

interface Props {
  onChange: (pickerData: PeoplePickerData) => void;
}

function Filters(props: Props): ReactElement {
  const [pickerData, setPickerData] = useState<PeoplePickerData>({
    schoolGuid: getCurrentSchoolRedux()?.schoolGuid ?? '',
  });
  useEffect(() => {
    props.onChange(pickerData);
  }, [pickerData]);
  return (
    <div className="border-bottom m-1 p-1">
      <Row>
        <Col>
          <ClassSelect
            onClassChanged={(classGuid) => {
              setPickerData((e) => ({ ...e, activeClassGuid: classGuid }));
            }}
          />
        </Col>
        <Col>
          <RoleSelect
            onRoleChanged={(r) => {
              setPickerData((e) => ({ ...e, schoolRole: r }));
            }}
          />
        </Col>
      </Row>
    </div>
  );
}
export default Filters;
