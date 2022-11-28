import {
  faArrowDown,
  faArrowLeft,
  faArrowRight,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import moment, { Moment } from 'moment';
import React, { ReactElement, useEffect, useState } from 'react';
import {
  Accordion,
  Button,
  Card,
  useAccordionButton,
  Row,
  Col,
  ListGroup,
  ListGroupItem,
} from 'react-bootstrap';

interface Event {
  startDate: Moment;
  endDate: Moment;
  title: string;
}

interface DayTileProps {
  day: Moment;
  events: Event[];
}

function CustomToggle({
  children,
  eventKey,
  onClick,
}: {
  children: any;
  eventKey: string;
  onClick: () => void;
}): ReactElement {
  const decoratedOnClick = useAccordionButton(eventKey, onClick);

  return (
    <Button onClick={decoratedOnClick} variant="outlined">
      {children}
    </Button>
  );
}

function DayTile(props: DayTileProps): ReactElement {
  return (
    <Col className="px-1">
      <Card className="my-1">
        <Card.Header className="fs-5">
          {moment(props.day).local().format('Do MMM')}
        </Card.Header>
        <Card.Body className="p-0">
          <ListGroup>
            {props.events
              .sort(
                (o, n) =>
                  o.startDate.toDate().getTime() -
                  n.startDate.toDate().getTime()
              )
              .map((evt, key) => (
                <ListGroupItem key={key}>
                  <Row>
                    <Col>
                      <Row className="text-center">
                        <Col>
                          <small>
                            {`${evt.startDate.local().format('LT')}`}
                          </small>
                        </Col>
                        <Col>
                          <small>{`${evt.endDate.local().format('LT')}`}</small>
                        </Col>
                      </Row>
                      <Row>
                        <Col>{evt.title}</Col>
                      </Row>
                    </Col>
                  </Row>
                </ListGroupItem>
              ))}
          </ListGroup>
        </Card.Body>
      </Card>
    </Col>
  );
}

interface Props {
  events: Event[];
}

function CalendarCollapsible(props: Props): ReactElement {
  const [isCollapsed, setIsCollapsed] = useState(true);
  const [startDate, setStartDate] = useState(moment());
  const [withWeekends, setWithWeekends] = useState(true);
  useEffect(() => {
    const stopDate = startDate
      .clone()
      .endOf('day')
      .add(1, 'week')
      .startOf('day');
    const currentlyShownEvents = props.events.filter((ev) =>
      ev.startDate.clone().isBetween(startDate.clone().startOf('day'), stopDate)
    );
    setWithWeekends(
      currentlyShownEvents.length === 0
        ? false
        : !!currentlyShownEvents
            .map((ev) => ev.startDate)
            .find((d) => d.isoWeekday())
    );
  }, [startDate]);
  return (
    <>
      <Accordion>
        <Card>
          <Card.Body className="mb-0 pb-0">
            <Row>
              <div className="d-flex justify-content-between">
                <Button
                  variant="outlined"
                  onClick={() =>
                    setStartDate((e) => e.clone().subtract(7, 'days'))
                  }
                >
                  {' '}
                  <FontAwesomeIcon icon={faArrowLeft} />
                </Button>
                <Button
                  variant="outlined"
                  onClick={() => setStartDate((e) => e.clone().add(7, 'days'))}
                >
                  <FontAwesomeIcon icon={faArrowRight} />
                </Button>
              </div>
            </Row>
            <Row>
              {[...Array(withWeekends ? 7 : 5)].map((e, i) => (
                <Col className="text-center" key={i}>
                  {moment().startOf('isoWeek').add(i, 'days').format('ddd')}
                </Col>
              ))}
            </Row>
            <Row>
              {[...Array(withWeekends ? 7 : 5)].map((e, i) => {
                const day = startDate
                  .clone()
                  .startOf('isoWeek')
                  .add(0 * 7 + i, 'days');
                return (
                  <DayTile
                    key={i}
                    day={day}
                    events={props.events.filter(
                      (event) => event.startDate.format('l') === day.format('l')
                    )}
                  />
                );
              })}
            </Row>
          </Card.Body>
          <Accordion.Collapse eventKey="0">
            <Card.Body className="my-0 py-0">
              <Row>
                {[...Array(withWeekends ? 7 : 5)].map((e, i) => {
                  const day = startDate
                    .clone()
                    .startOf('isoWeek')
                    .add(1 * 7 + i, 'days');
                  return (
                    <DayTile
                      key={i}
                      day={day}
                      events={props.events.filter(
                        (event) =>
                          event.startDate.format('l') === day.format('l')
                      )}
                    />
                  );
                })}
              </Row>
            </Card.Body>
          </Accordion.Collapse>
          <Card.Body className="mt-0 pt-0">
            <div className="d-flex justify-content-end">
              <CustomToggle
                eventKey={'0'}
                onClick={() => setIsCollapsed((c) => !c)}
              >
                <FontAwesomeIcon
                  icon={faArrowDown}
                  className={isCollapsed ? '' : 'rotate-180'}
                />
              </CustomToggle>
            </div>
          </Card.Body>
        </Card>
      </Accordion>
    </>
  );
}

export default CalendarCollapsible;
