import { SET_ANSWER } from "../../Constraints/actionTypes";
import Field from "../../Interfaces/Common/Field";
import ReduxGetAnswers from "../ReduxGet/ReduxGetAnswers";
import { store } from "../store";

export default (field: Field) => {
    if (!field) return;
    const fields = ReduxGetAnswers(store.getState()) ?? [];
    const index = fields.indexOf(fields.find(f => f.uuid == field.uuid)!);
    store.dispatch({ type: SET_ANSWER, payload: { field: { ...field, onBlur: undefined }, index } });
};