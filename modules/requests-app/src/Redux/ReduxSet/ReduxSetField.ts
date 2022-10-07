import Field from "../../Interfaces/Common/Field";
import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxSetFields from "./ReduxSetFields";

export default (field: Field) => {
    if (!field) return;
    const fields = ReduxGetFields(store.getState());
    const index = fields.indexOf(fields.find(f => f.uuid == field.uuid)!);

    const newFields = fields.slice();
    if (index < 0)
        newFields.push(field);
    else
        newFields[index] = field;
    ReduxSetFields(newFields);
};