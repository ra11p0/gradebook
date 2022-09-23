import { currentSchoolProxy } from "./currentSchoolProxy";

export const isSchoolSelectedProxy = (state: any): boolean => currentSchoolProxy(state) != null;