import ReduxGetFields from "../ReduxGet/ReduxGetFields";
import { store } from "../store";
import ReduxSetFields from "./ReduxSetFields";

export default (uuid: string) => {
    let fields = ReduxGetFields(store.getState()).filter(f => f.uuid != uuid);
    ReduxSetFields([...fields]);
};