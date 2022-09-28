import getSchoolsListReduxProxy from "./getSchoolsListReduxProxy";

export default (state: any): boolean => getSchoolsListReduxProxy(state)?.length != 0;
