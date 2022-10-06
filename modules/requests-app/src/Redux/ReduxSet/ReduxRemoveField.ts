import Field from "../../Interfaces/Common/Field";
import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxSetFields from "./ReduxSetFields";

export default (field: Field) => {
    let fields = ReduxGetFields(store.getState()).filter(f => f.uuid != field.uuid);
    ReduxSetFields([...fields]);
};