import ActionTypes from "../ActionTypes/newEducationCycleFormActionTypes";

export type State = {

}

const defaultState: State = {}

export default (state: State = defaultState, action: { type: ActionTypes, payload: any }): State => {
    switch (action.type) {
        default: return state
    }
}
