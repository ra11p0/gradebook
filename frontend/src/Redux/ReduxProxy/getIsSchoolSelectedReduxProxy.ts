import { currentSchoolProxy } from "./getCurrentSchoolReduxProxy";

export const isSchoolSelectedProxy = (state: any): boolean => currentSchoolProxy(state) != null;