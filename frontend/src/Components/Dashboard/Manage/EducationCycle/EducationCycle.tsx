import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { Route, Routes } from 'react-router';
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';
import getHasPermissionRedux from '../../../../Redux/ReduxQueries/account/getHasPermissionRedux';
import { GlobalState } from '../../../../store';
import CyclesList from './CyclesList/CyclesList';
import NewCycleForm from './NewCycleForm/NewCycleForm';

interface Props {
  canCreateNewEducationCycle: boolean;
}

function EducationCycle(props: Props): ReactElement {
  return (
    <>
      <Routes>
        <Route path="*" element={<CyclesList />} />
        {props.canCreateNewEducationCycle && (
          <Route path="new" element={<NewCycleForm />} />
        )}
      </Routes>
    </>
  );
}

export default connect(
  (state: GlobalState) => ({
    canCreateNewEducationCycle: getHasPermissionRedux(
      [PermissionLevelEnum.EducationCycles_CanCreateAndDelete],
      state
    ),
  }),
  () => ({})
)(EducationCycle);
