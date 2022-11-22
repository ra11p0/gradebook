import { store } from "../../../store";
import getOpenStagesRedux from "../../ReduxQueries/newEducationCycleForm/getOpenStagesRedux";
import setOpenStagesRedux from "./setOpenStagesRedux";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

export default (stageUuid: string, dispatch: any = store.dispatch) => {
    var currentlyOpen = getOpenStagesRedux();
    setOpenStagesRedux([...currentlyOpen, stageUuid]);
};