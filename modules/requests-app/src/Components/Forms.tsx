import { Provider } from "react-redux";
import { store } from "../Redux/store";
import Loading from "./Common/Loading";
import "../i18n/config";
import "bootstrap/dist/css/bootstrap.css";
import Editor from "./Editor/Editor";
type Props = {};

function Forms(props: Props) {
  return (
    <Provider store={store}>
      <div className="w-50">
        <Loading isReady={true}>
          <Editor />
        </Loading>
      </div>
    </Provider>
  );
}

export default Forms;
