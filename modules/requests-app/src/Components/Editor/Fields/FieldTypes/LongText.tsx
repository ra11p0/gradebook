import React, { useState } from "react";
import { Form } from "react-bootstrap";

type Props = { value?: any };

function LongText(props: Props) {
  const [value, setValue] = useState(props.value as string);
  return (
    <>
      <Form.Control
        as={"textarea"}
        value={value}
        onChange={(newValue) => {
          setValue(newValue.target.value);
        }}
      />
    </>
  );
}

export default LongText;
