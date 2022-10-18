import Field from "../../Interfaces/Common/Field";
import ReduxSetAnswer from "./ReduxSetAnswer";

export default (fields: Field[]) => {
    /*store.dispatch({
        type: SET_ANSWERS, payload: fields.map(field => ({ ...field, onBlur: undefined }))
    })*/
    fields.forEach(field => ReduxSetAnswer(field))
};