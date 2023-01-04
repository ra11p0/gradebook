import { Checkbox } from '@mui/material';
import React, { ReactElement } from 'react';

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
        onClick={() => {
          props.selectedPeople.includes(props.guid)
            ? props.setSelectedPeople((s) => s.filter((p) => p !== props.guid))
            : props.setSelectedPeople((s) => [...s, props.guid]);
        }}
      >
        <div className="my-auto">{`${props.name} ${props.surname}`}</div>
        <div>
          <Checkbox
            checked={props.selectedPeople.includes(props.guid)}
            onChange={(e, o) =>
              o
                ? props.setSelectedPeople((s) => [...s, props.guid])
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
