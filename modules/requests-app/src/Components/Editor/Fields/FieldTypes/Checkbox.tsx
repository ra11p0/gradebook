import React, { useState } from "react";
import { Col, Form, Row } from "react-bootstrap";

type Props = {
    value?: boolean[],
    labels: string[],
    distinctValues?: boolean;
};

function Checkbox(props: Props) {
    const [values, setValues] = useState<boolean[]>(props.value ?? [false]);
    return (
        <div>
            {
                props.labels.map((c, i) =>
                    <Row key={i}>
                        <Col>
                            <Form.Check
                                key={i}
                                type="checkbox"
                                checked={values[i] ?? false}
                                onChange={(newValue) => {
                                    const newValues = (props.distinctValues ?? false) ? values.map(() => false) : values.slice();
                                    newValues[i] = newValue.target.checked;
                                    setValues(newValues);
                                }}
                                label={props.labels[i]}
                            />

                        </Col>
                    </Row>)
            }
        </div>
    );
}

export default Checkbox;
