import { faPlus, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { uniqueId } from "lodash";
import React, { ReactElement, useState } from "react";
import { Button, Col, Row } from "react-bootstrap";

interface Props {
  map: (uuid: string, index: number) => ReactElement;
  list?: string[];
  onAdded?: (uuid: string) => void;
  onRemoved?: (uuid: string) => void;
};

function DynamicList(props: Props) {
  const [list, setList] = useState<string[]>([]);
  const remove = (uuid: string) => {
    if (!props.list) setList(list.filter((el) => el != uuid));
    if (props.onRemoved) props.onRemoved(uuid);
  };
  const add = () => {
    const newElUuid = uniqueId();
    if (!props.list) setList([...list, newElUuid]);
    if (props.onAdded) props.onAdded(newElUuid);
  };
  return (
    <div>
      {(props.list ?? list).map((uuid, index) => (
        <Row key={index} className="my-1 py-1 border-bottom">
          <Col className="d-flex justify-content-between">
            <div className="w-100">
              {
                props.map(uuid, index)
              }
            </div>
            <div>
              <Button variant="danger" onClick={() => remove(uuid)}>
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </div>
          </Col>
        </Row>
      ))
      }
      <Row>
        <Col className="d-flex justify-content-end">
          <Button variant="success" onClick={() => add()}>
            <FontAwesomeIcon icon={faPlus} />
          </Button>
        </Col>
      </Row>
    </div >
  );
}

export default DynamicList;
