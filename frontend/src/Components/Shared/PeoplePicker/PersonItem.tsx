import { Checkbox } from '@mui/material';
import { ReactElement } from 'react';

interface Props {
  guid: string;
  selectedPeople: string[];
  name: string;
  surname: string;
  setSelectedPeople: (setFn: (e: string[]) => string[]) => void;
}

function PersonItem(props: Props): ReactElement {
  return (
    <>
      <div
        className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
        data-person-full-name={`${props.name} ${props.surname}`}
        onClick={() => {
          props.selectedPeople.includes(props.guid)
            ? props.setSelectedPeople((s) => s.filter((p) => p !== props.guid))
            : props.setSelectedPeople((s) => [...s, props.guid]);
        }}
      >
        <div className="my-auto">{`${props.name} ${props.surname}`}</div>
        <div>
          <Checkbox
            id={`personCheckbox-${props.guid}`}
            checked={props.selectedPeople.includes(props.guid)}
            onChange={(e, o) =>
              o
                ? props.setSelectedPeople((s) =>
                    [...s, props.guid].filter(
                      (el, elIndex, arr) => arr.indexOf(el) === elIndex
                    )
                  )
                : props.setSelectedPeople((s) =>
                    s.filter((p) => p !== props.guid)
                  )
            }
          />
        </div>
      </div>
    </>
  );
}
export default PersonItem;
