import { combineReducers } from 'redux';
import common from './Redux/Reducers/accountReducer'

export default combineReducers({
  common: common,
});