import { useRef, useState } from "react";
import FieldEditor from "./FieldEditor";
import FieldPreview from "./FieldPreview";
import FieldInterface from "../../../Interfaces/Common/Field";
import ReduxSetCurrentlyEdited from "../../../Redux/ReduxSet/ReduxSetCurrentlyEdited";
import { connect } from "react-redux";
import ReduxGetCurrentlyEdited from "../../../Redux/ReduxGet/ReduxGetCurrentlyEdited";
import ReduxGetFields from "../../../Redux/ReduxGet/ReduxGetFields";
import { store } from "../../../Redux/store";
import { useDrag, useDrop } from "react-dnd";
import update from "immutability-helper";
import ReduxSetFields from "../../../Redux/ReduxSet/ReduxSetFields";

type Props = {
  field: FieldInterface;
  currentlyEdited: string;
  onRemoveFieldHandler: () => void;
};

const type = "Image";

function Field(props: Props) {
  const ref = useRef(null); // Initialize the reference

  // useDrag will be responsible for making an element draggable. It also expose, isDragging method to add any styles while dragging
  const [{ /*isDragging*/ }, drag] = useDrag(() => ({
    // what type of item this to determine if a drop target accepts it
    type: type,
    // data of the item to be available to the drop methods
    item: { id: props.field.uuid, index: ReduxGetFields(store.getState()).indexOf(props.field) },
    // method to collect additional data for drop handling like whether is currently being dragged
    collect: (monitor) => {
      return {
        isDragging: monitor.isDragging(),
      };
    },
  }));
  const [, drop] = useDrop({
    // accept receives a definition of what must be the type of the dragged item to be droppable
    accept: type,
    // This method is called when we hover over an element while dragging
    hover(item: any) { // item is the dragged element
      if (!ref.current) {
        return;
      }
      const dragIndex = item.index;
      // current element where the dragged element is hovered on
      const hoverIndex = ReduxGetFields(store.getState()).indexOf(props.field);
      // If the dragged element is hovered in the same place, then do nothing
      if (dragIndex === hoverIndex) {
        return;
      }
      // If it is dragged around other elements, then move the image and set the state with position changes
      moveImage(dragIndex, hoverIndex);
      /*
        Update the index for dragged item directly to avoid flickering
        when the image was half dragged into the next
      */
      item.index = hoverIndex;
    }
  });
  const moveImage = (dragIndex: any, hoverIndex: any) => {
    // Get the dragged element
    const draggedImage = ReduxGetFields(store.getState())[dragIndex];
    /*
      - copy the dragged image before hovered element (i.e., [hoverIndex, 0, draggedImage])
      - remove the previous reference of dragged element (i.e., [dragIndex, 1])
      - here we are using this update helper method from immutability-helper package
    */
    ReduxSetFields(
      update(ReduxGetFields(store.getState()), {
        $splice: [[dragIndex, 1], [hoverIndex, 0, draggedImage]]
      })
    );
  };
  /* 
    Initialize drag and drop into the element using its reference.
    Here we initialize both drag and drop on the same element (i.e., Image component)
  */
  drag(drop(ref));

  const [isHovered, setIsHovered] = useState(false);

  return (
    <div ref={ref}
      className={(isHovered ? "border rounded-3 shadow " : "") + " m-2 p-2"}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}>

      {props.currentlyEdited == props.field.uuid ? (
        <FieldEditor
          field={props.field}
          onAbortEditingHandler={() => {
            ReduxSetCurrentlyEdited("");
          }}
          onFinishEditingHandler={() => ReduxSetCurrentlyEdited("")}
        />
      ) : (
        <FieldPreview
          field={props.field}
          onRemoveFieldHandler={props.onRemoveFieldHandler}
          onEditFieldHandler={() => ReduxSetCurrentlyEdited(props.field.uuid)}
          isHovered={isHovered}
        />
      )}

    </div>
  );
}

export default connect((state) => ({
  currentlyEdited: ReduxGetCurrentlyEdited(state),
}))(Field);
