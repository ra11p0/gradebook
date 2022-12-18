import getSchoolsListReduxProxy from './getSchoolsListRedux';

export default (state: any): boolean =>
  getSchoolsListReduxProxy(state)?.length !== 0;
