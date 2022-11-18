import { configureStore } from "@reduxjs/toolkit";
import reducer from "./reducer";
import { State as commonState } from './Redux/Reducers/accountReducer'
import { State as newEducationCycleFormState } from './Redux/Reducers/newEducationCycleFormReducer'

export type GlobalState = {
    common: commonState
    newEducationCycleForm: newEducationCycleFormState
}

export const store = configureStore({ reducer });
