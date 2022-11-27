import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import React, { ReactElement } from 'react';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import GetSchoolResponse from '../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse';
import getCurrentSchoolReduxProxy from '../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import getSchoolsListReduxProxy from '../../Redux/ReduxQueries/account/getSchoolsListRedux';
import setSchoolsListReduxWrapper, {
  setSchoolsListAction,
} from '../../Redux/ReduxCommands/account/setSchoolsListRedux';
import setSchoolReduxWrapper, {
  setSchoolAction,
} from '../../Redux/ReduxCommands/account/setSchoolRedux';
import { GlobalState } from '../../store';

interface SchoolSelectProps {
  className?: string;
  currentSchool?: any;
  schoolsList: GetSchoolResponse[] | null;
  setSchool: (action: setSchoolAction) => void;
  setSchoolsList: (action: setSchoolsListAction) => void;
}
function SchoolSelect(props: SchoolSelectProps): ReactElement {
  const { t } = useTranslation('schoolSelect');

  return (
    <div className={`${props.className ?? ''}`}>
      <FormControl sx={{ m: 1, minWidth: 380 }}>
        <InputLabel>{t('selectSchool')}</InputLabel>
        <Select
          value={props.currentSchool?.schoolGuid ?? ''}
          label={t('selectSchool')}
          onChange={(change) => {
            const selectedSchool = props.schoolsList?.find(
              (school) => change.target.value === school.guid
            );
            if (!selectedSchool) {
              props.setSchool({ schoolName: '', schoolGuid: '' });
              return;
            }
            props.setSchool({
              schoolGuid: selectedSchool.guid,
              schoolName: selectedSchool.name,
            });
          }}
        >
          {props.schoolsList?.map((school) => (
            <MenuItem value={school.guid} key={school.guid}>
              {school.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
}
export default connect(
  (state: GlobalState) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
    schoolsList: getSchoolsListReduxProxy(state),
  }),
  (dispatch) => ({
    setSchool: async (action: setSchoolAction) =>
      await setSchoolReduxWrapper(dispatch, action),
    setSchoolsList: (action: setSchoolsListAction) =>
      setSchoolsListReduxWrapper(dispatch, action),
  })
)(SchoolSelect);
