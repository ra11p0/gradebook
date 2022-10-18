import { Provider } from "react-redux";
import { store } from "../Redux/store";
import Loading from "./Common/Loading";
import "../i18n/config";
import "bootstrap/dist/css/bootstrap.css";
import "react-datepicker/dist/react-datepicker.css";
import Editor from "./Editor/Editor";
import Field from "../Interfaces/Common/Field";
import ApplicationModes from "../Constraints/ApplicationModes";
import ReduxSetFields from "../Redux/ReduxSet/ReduxSetFields";
import Filler from "./Filler/Filler";
import { useEffect } from "react";
import FormFilledResult from "../Interfaces/Common/FormFilledResult";


type Props = {
  mode: ApplicationModes,
  fields?: Field[],
  onSubmit: (result: FormFilledResult | Field[]) => void;
  onDiscard: () => void;
};

function Forms(props: Props) {
  useEffect(() => {
    ReduxSetFields(props.fields ?? []);
  }, [props.fields])

  return (
    <Provider store={store}>
      <div className="w-50">
        <Loading isReady={true}>
          <>
            {props.mode == ApplicationModes.Edit &&
              <Editor
                onSubmit={props.onSubmit}
                onDiscard={props.onDiscard} />
            }
            {
              (props.mode == ApplicationModes.Fill || props.mode == ApplicationModes.Preview) &&
              <Filler onSubmit={props.onSubmit}
                mode={props.mode}
              />
            }
          </>
        </Loading>
      </div>
    </Provider>
  );
}

export default Forms;
