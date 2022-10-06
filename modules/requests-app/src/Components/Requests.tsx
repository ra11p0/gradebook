import { Provider } from "react-redux";
import { store } from "../Redux/store";
import Loading from "./Common/Loading";
import "../i18n/config";
import "bootstrap/dist/css/bootstrap.css";
import Editor from "./Editor/Editor";
type Props = {};

function Requests(props: Props) {
  return (
    <Provider store={store}>
      <Loading isReady={true}>
        <Editor></Editor>
      </Loading>
    </Provider>
  );
}

export default Requests;
