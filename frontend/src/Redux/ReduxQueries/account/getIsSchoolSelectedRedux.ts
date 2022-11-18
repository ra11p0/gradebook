import getCurrentSchoolReduxProxy from "./getCurrentSchoolRedux";

export default (state: any): boolean => getCurrentSchoolReduxProxy(state) != null;