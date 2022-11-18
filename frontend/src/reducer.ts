import { combineReducers } from 'redux';
import common from './Redux/Reducers/accountReducer'
import newEducationCycleForm from './Redux/Reducers/newEducationCycleFormReducer'

export default combineReducers({
  common: common,
  newEducationCycleForm: newEducationCycleForm
});