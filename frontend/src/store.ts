import { configureStore } from "@reduxjs/toolkit";
import reducer from "./reducer";
import { State as commonState } from './Redux/Reducers/accountReducer'

export type GlobalState = {
    common: commonState
}

export const store = configureStore({ reducer });
