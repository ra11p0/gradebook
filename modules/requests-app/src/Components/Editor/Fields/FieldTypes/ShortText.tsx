import React, { useState } from "react";
import { Form } from "react-bootstrap";

type Props = { value?: any };

function ShortText(props: Props) {
  const [value, setValue] = useState(props.value as string);
  return (
    <>
      <Form.Control
        value={value}
        onChange={(newValue) => {
          setValue(newValue.target.value);
        }}
      />
    </>
  );
}

export default ShortText;
