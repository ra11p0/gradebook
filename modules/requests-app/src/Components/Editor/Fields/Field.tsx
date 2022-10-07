import { useRef, useState } from "react";
import FieldEditor from "./FieldEditor";
import FieldPreview from "./FieldPreview";
import FieldInterface from "../../../Interfaces/Common/Field";
import ReduxSetCurrentlyEdited from "../../../Redux/ReduxSet/ReduxSetCurrentlyEdited";
import { connect } from "react-redux";
import ReduxGetCurrentlyEdited from "../../../Redux/ReduxGet/ReduxGetCurrentlyEdited";
import ReduxGetFields from "../../../Redux/ReduxGet/ReduxGetFields";
import { store } from "../../../Redux/store";
import ReduxRemoveField from "../../../Redux/ReduxSet/ReduxRemoveField";
import ReduxFixFields from "../../../Redux/ReduxSet/ReduxFixFields";
import { ListGroup } from "react-bootstrap";
import { useDrag, useDrop } from "react-dnd";
import update from "immutability-helper";
import ReduxSetFields from "../../../Redux/ReduxSet/ReduxSetFields";
import ReduxSetField from "../../../Redux/ReduxSet/ReduxSetField";

type Props = {
  field: FieldInterface;
  currentlyEdited: string;
  onRemoveFieldHandler: () => void;
};

const type = "Field";

function Field(props: Props) {
  const ref = useRef(null); // Initialize the reference

  // useDrop hook is responsible for handling whether any item gets hovered or dropped on the element
  const [, drop] = useDrop({
    // Accept will make sure only these element type can be droppable on this element
    accept: type,
    hover(item: any) {
      //console.dir(item);
      if (!ref.current) {
        return;
      }
      const dragIndex = item.order;
      // current element where the dragged element is hovered on
      if (!props.field) return;
      const hoverIndex = props.field.order;
      // If the dragged element is hovered in the same place, then do nothing
      if (dragIndex === hoverIndex) {
        return;
      }
      // If it is dragged around other elements, then move the image and set the state with position changes
      ((dragIndex, hoverIndex) => {
        // Get the dragged element
        const draggedField = ReduxGetFields(store.getState()).find((f) => f.order === dragIndex);
        /*
          - copy the dragged image before hovered element (i.e., [hoverIndex, 0, draggedImage])
          - remove the previous reference of dragged element (i.e., [dragIndex, 1])
          - here we are using this update helper method from immutability-helper package
        */
        /*console.log(dragIndex);
        console.log(hoverIndex);*/

        [
          ...ReduxGetFields(store.getState())
            .filter((f) => f.uuid != draggedField?.uuid)
            .map((f) => ({ ...f, order: f.order > hoverIndex ? f.order + 1 : f.order })),
          { ...draggedField!, order: hoverIndex },
        ].forEach((f) => ReduxSetField(f));
      })(dragIndex, hoverIndex);
      ReduxFixFields();
      //moveImage(dragIndex, hoverIndex);
      /*
      Update the index for dragged item directly to avoid flickering
      when the image was half dragged into the next
    */
      item.order = hoverIndex;
    },
  });

  // useDrag will be responsible for making an element draggable. It also expose, isDragging method to add any styles while dragging
  const [{ isDragging }, drag] = useDrag(() => ({
    // what type of item this to determine if a drop target accepts it
    type: type,
    // data of the item to be available to the drop methods
    item: { order: props.field.order },
    // method to collect additional data for drop handling like whether is currently being dragged
    collect: (monitor) => {
      return {
        isDragging: monitor.isDragging(),
      };
    },
  }));

  /* 
    Initialize drag and drop into the element using its reference.
    Here we initialize both drag and drop on the same element (i.e., Image component)
  */
  drag(drop(ref));

  return (
    <ListGroup.Item ref={ref}>
      {props.currentlyEdited == props.field.uuid ? (
        <FieldEditor
          field={props.field}
          onAbortEditingHandler={() => {
            ReduxSetCurrentlyEdited("");
            ReduxFixFields();
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
    </ListGroup.Item>
  );
}

export default connect((state) => ({
  currentlyEdited: ReduxGetCurrentlyEdited(state),
}))(Field);
