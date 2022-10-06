import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxRemoveField from "./ReduxRemoveField";

export default () => {
    let fields = ReduxGetFields(store.getState()).filter(f => f.name.length === 0);
    fields.forEach(f => ReduxRemoveField(f.uuid));
};