import { faPlus, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { uniqueId } from "lodash";
import React, { ReactElement, useState } from "react";
import { Button, Col, Row } from "react-bootstrap";

type Props = {
  item: (uuid: string) => ReactElement;
  onAdded?: (uuid: string) => void;
  onRemoved?: (uuid: string) => void;
};

function DynamicList(props: Props) {
  const [list, setList] = useState<{ el: ReactElement; uuid: string }[]>([]);
  const remove = (uuid: string) => {
    setList(list.filter((el) => el.uuid != uuid));
    if (props.onRemoved) props.onRemoved(uuid);
  };
  const add = () => {
    const newElUuid = uniqueId();
    const newEl = { el: props.item(newElUuid), uuid: newElUuid };
    setList([...list, newEl]);
    if (props.onAdded) props.onAdded(newEl.uuid);
  };
  return (
    <div>
      {list.map((e) => (
        <Row key={e.uuid} className="my-1 py-1 border-bottom">
          <Col className="d-flex justify-content-between">
            <div className="w-100">{e.el}</div>
            <div>
              <Button variant="danger" onClick={() => remove(e.uuid)}>
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </div>
          </Col>
        </Row>
      ))}
      <Row>
        <Col className="d-flex justify-content-end">
          <Button variant="success" onClick={() => add()}>
            <FontAwesomeIcon icon={faPlus} />
          </Button>
        </Col>
      </Row>
    </div>
  );
}

export default DynamicList;
