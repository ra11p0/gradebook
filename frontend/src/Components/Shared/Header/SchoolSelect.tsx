import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import GetSchoolResponse from '../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse';
import getCurrentSchoolReduxProxy, {
  GetCurrentSchoolReduxProxyResult,
} from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import getSchoolsListReduxProxy from '../../../Redux/ReduxQueries/account/getSchoolsListRedux';
import setSchoolRedux from '../../../Redux/ReduxCommands/account/setSchoolRedux';
import { GlobalState } from '../../../store';
import ReactSelect from 'react-select';

interface SchoolSelectProps {
  currentSchool?: GetCurrentSchoolReduxProxyResult;
  schoolsList: GetSchoolResponse[] | undefined;
}
function SchoolSelect(props: SchoolSelectProps): ReactElement {
  return (
    <ReactSelect
      id="schoolSelect"
      isSearchable={false}
      isLoading={!props.schoolsList}
      onChange={async (e): Promise<void> => {
        if (!e!.value) return;
        await setSchoolRedux(e!.value);
      }}
      value={(() => {
        if (!props.schoolsList) return;
        const school = props.schoolsList.find(
          (e) => e.guid === props.currentSchool?.schoolGuid
        );
        if (!school) return;

        return { label: school.name, value: school.guid };
      })()}
      options={props.schoolsList?.map((e) => ({
        label: e.name,
        value: e.guid,
      }))}
    />
  );
}
export default connect(
  (state: GlobalState) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
    schoolsList: getSchoolsListReduxProxy(state),
  }),
  () => ({})
)(SchoolSelect);
