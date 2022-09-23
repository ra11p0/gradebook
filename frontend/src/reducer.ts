import { combineReducers } from 'redux';
import common from './Redux/Reducers/Account/accountReducer'

export default combineReducers({
  common: common
});