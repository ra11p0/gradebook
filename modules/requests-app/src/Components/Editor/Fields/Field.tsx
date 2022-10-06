import { useState } from "react";
import FieldEditor from "./FieldEditor";
import FieldPreview from "./FieldPreview";
import FieldInterface from "../../../Interfaces/Common/Field";
import ReduxSetCurrentlyEdited from "../../../Redux/ReduxSet/ReduxSetCurrentlyEdited";
import { connect } from "react-redux";
import ReduxGetCurrentlyEdited from "../../../Redux/ReduxGet/ReduxGetCurrentlyEdited";
import ReduxGetFields from "../../../Redux/ReduxGet/ReduxGetFields";
import { store } from "../../../Redux/store";
import ReduxRemoveField from "../../../Redux/ReduxSet/ReduxRemoveField";
import ReduxRemoveInvalid from "../../../Redux/ReduxSet/ReduxRemoveInvalid";

type Props = {
  field: FieldInterface;
  currentlyEdited: string;
  onRemoveFieldHandler: () => void;
};

function Field(props: Props) {
  return (
    <>
      {props.currentlyEdited == props.field.uuid ? (
        <FieldEditor
          field={props.field}
          onAbortEditingHandler={() => {
            ReduxRemoveInvalid();
            ReduxSetCurrentlyEdited("");
          }}
          onFinishEditingHandler={() => ReduxSetCurrentlyEdited("")}
        />
      ) : (
        <FieldPreview
          field={props.field}
          onRemoveFieldHandler={props.onRemoveFieldHandler}
          onEditFieldHandler={() => ReduxSetCurrentlyEdited(props.field.uuid)}
        />
      )}
    </>
  );
}

export default connect((state) => ({
  currentlyEdited: ReduxGetCurrentlyEdited(state),
}))(Field);
