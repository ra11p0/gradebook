import { schoolsListProxy } from "./schoolsListProxy";

export const isUserActivatedProxy = (state: any): boolean => schoolsListProxy(state)?.length != 0;
