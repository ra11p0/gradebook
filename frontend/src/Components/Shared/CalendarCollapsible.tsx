import { faArrowDown, faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import moment from 'moment';
import React, { ReactElement, useEffect, useState } from 'react'
import { Accordion, Button, Card, useAccordionButton, Row, Col, ListGroup, ListGroupItem } from 'react-bootstrap'

type Props = {}

function CustomToggle({ children, eventKey, onClick }: { children: any, eventKey: string, onClick: () => void }) {
    const decoratedOnClick = useAccordionButton(eventKey, onClick);

    return (
        <Button onClick={decoratedOnClick} variant='outlined' >
            {children}
        </Button>
    );
}

function DayTile(props: { date: Date }) {
    return (
        <Col>
            <Card className='my-1'>
                <Card.Header>
                    {moment(props.date).local().format('Do MMM')}
                </Card.Header>
                <Card.Body className='p-0'>
                    <ListGroup>
                        <ListGroupItem>
                            sth
                        </ListGroupItem>
                        <ListGroupItem>
                            sth
                        </ListGroupItem>
                        <ListGroupItem>
                            sth
                        </ListGroupItem>
                    </ListGroup>
                </Card.Body>
            </Card>
        </Col>
    );
}

function CalendarCollapsible({ }: Props) {
    const [isCollapsed, setIsCollapsed] = useState(true);
    const [startDate, setStartDate] = useState(moment());
    useEffect(() => { }, [startDate]);
    return (
        <>
            <Accordion>
                <Card>
                    <Card.Body className='mb-0 pb-0'>
                        <Row>
                            <div className='d-flex justify-content-between'>
                                <Button variant='outlined' onClick={() => setStartDate(e => e.clone().add(7, 'days'))} > <FontAwesomeIcon icon={faArrowLeft} /></Button>
                                <Button variant='outlined' onClick={() => setStartDate(e => e.clone().subtract(7, 'days'))} ><FontAwesomeIcon icon={faArrowRight} /></Button>
                            </div>
                        </Row>
                        <Row>
                            {
                                [...Array(7)].map((e, i) => (
                                    <Col className='text-center'>
                                        {moment().weekday(0).add(i, 'days').format('dddd')}
                                    </Col>))
                            }
                        </Row>
                        <Row>
                            {
                                [...Array(7)].map((e, i) => (<DayTile date={startDate.clone().weekday(0).add(0 * 7 + i, 'days').toDate()} />))
                            }
                        </Row>
                    </Card.Body>
                    <Accordion.Collapse eventKey="0">
                        <Card.Body className='my-0 py-0'>
                            <Row>
                                {
                                    [...Array(7)].map((e, i) => (<DayTile date={startDate.clone().weekday(0).add(1 * 7 + i, 'days').toDate()} />))
                                }
                            </Row>
                        </Card.Body>
                    </Accordion.Collapse>
                    <Card.Body className='mt-0 pt-0'>
                        <div className='d-flex justify-content-end'>
                            <CustomToggle eventKey={"0"} onClick={() => setIsCollapsed(c => !c)} >
                                <FontAwesomeIcon icon={faArrowDown} className={isCollapsed ? "" : "rotate-180"} />
                            </CustomToggle>
                        </div>
                    </Card.Body>
                </Card>
            </Accordion>
        </>
    )
}

export default CalendarCollapsible