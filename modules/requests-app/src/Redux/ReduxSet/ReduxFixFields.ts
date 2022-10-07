import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxRemoveField from "./ReduxRemoveField";
import ReduxSetField from "./ReduxSetField";

export default () => {
    let fieldsToRemove = ReduxGetFields(store.getState()).filter(f => f.name.length === 0);
    fieldsToRemove.forEach(f => ReduxRemoveField(f.uuid));
    ReduxGetFields(store.getState()).forEach((f, i) => ReduxSetField({ ...f, order: i + 1 }));
};