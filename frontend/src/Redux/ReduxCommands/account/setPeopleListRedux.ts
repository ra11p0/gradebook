import ActionTypes from '../../ActionTypes/accountActionTypes';

const setPeopleList = {
  type: ActionTypes.SetPeopleList,
};

export interface setPeopleListAction {
  peopleList: [];
}

export default (dispatch: any, action: setPeopleListAction): void => {
  dispatch({ ...setPeopleList, payload: { ...action } });
};
