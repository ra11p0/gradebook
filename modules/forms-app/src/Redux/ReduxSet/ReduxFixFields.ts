import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxSetFields from "./ReduxSetFields";

export default () => {
    const fieldsToRemove = ReduxGetFields(store.getState()).slice().filter(f => f.name.length === 0).map(f => f.uuid);
    const newFields = ReduxGetFields(store.getState()).slice().filter(f => !fieldsToRemove.includes(f.uuid));
    ReduxSetFields(newFields);
};