import { schoolsListProxy } from "./getSchoolsListReduxProxy";

export const isUserActivatedProxy = (state: any): boolean => schoolsListProxy(state)?.length != 0;
