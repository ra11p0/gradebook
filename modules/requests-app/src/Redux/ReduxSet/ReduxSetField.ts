import Field from "../../Interfaces/Common/Field";
import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxSetFields from "./ReduxSetFields";

export default (field: Field) => {
    if (!field) return;
    let fields = ReduxGetFields(store.getState()).filter(f => f.uuid != field.uuid);
    ReduxSetFields([...fields, field]);
};