import { Provider } from "react-redux";
import { store } from "../Redux/store";
import Loading from "./Common/Loading";
import "../i18n/config";
import "bootstrap/dist/css/bootstrap.css";
import Editor from "./Editor/Editor";
import Field from "../Interfaces/Common/Field";
import FieldTypes from "../Constraints/FieldTypes";
type Props = {};

function Requests({}: Props) {
  const fields: Field[] = [
    {
      uuid: "aaaaa",
      name: "Imię i nazwisko",
      type: FieldTypes.ShortText,
      value: "Marian broda",
    },
    {
      uuid: "bbbbb",
      name: "Odpowiedz na pytanie",
      type: FieldTypes.LongText,
      value: "To jest bardzo dluga odpowiedz na pytanie w tym momencie prosze bardzo.",
    },
  ];
  return (
    <Provider store={store}>
      <Loading isReady={true}>
        <Editor fields={fields}></Editor>
      </Loading>
    </Provider>
  );
}

export default Requests;
