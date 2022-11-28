import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import _, { uniqueId } from 'lodash';
import React, { ReactElement, useState } from 'react';
import { Button, Col, Row } from 'react-bootstrap';

interface Props {
  map: (uuid: string, index: number) => ReactElement;
  list?: string[];
  onAdded?: (uuid: string) => void | Promise<void>;
  onRemoved?: (uuid: string, index: number) => void | Promise<void>;
}

function DynamicList(props: Props): ReactElement {
  const [list, setList] = useState<string[]>([]);
  const remove = async (uuid: string, index: number): Promise<void> => {
    if (props.list == null) setList(_.remove(list, (el) => el !== uuid));
    if (props.onRemoved != null) await props.onRemoved(uuid, index);
  };
  const add = async (): Promise<void> => {
    const newElUuid = uniqueId();
    if (props.list == null) setList([...list, newElUuid]);
    if (props.onAdded != null) await props.onAdded(newElUuid);
  };

  return (
    <div>
      {(props.list ?? list).map((uuid, index) => (
        <Row key={index} className="my-1 py-1 border-bottom">
          <Col className="d-flex justify-content-between">
            <div className="w-100">{props.map(uuid, index)}</div>
            <div>
              <Button
                variant="danger"
                onClick={async () => {
                  await remove(uuid, index);
                }}
              >
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </div>
          </Col>
        </Row>
      ))}
      <Row>
        <Col className="d-flex justify-content-end">
          <Button
            data-testid="dynamicListAddNewButton"
            variant="success"
            onClick={async () => {
              await add();
            }}
          >
            <FontAwesomeIcon icon={faPlus} />
          </Button>
        </Col>
      </Row>
    </div>
  );
}

export default DynamicList;
