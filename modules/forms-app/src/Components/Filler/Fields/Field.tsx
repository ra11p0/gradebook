import { Col, Form, FormLabel, Row } from 'react-bootstrap'
import FieldTypes from '../../../Constraints/FieldTypes'
import FieldInterface from '../../../Interfaces/Common/Field'
import Checkbox from '../../Editor/Fields/FieldTypes/Checkbox'
import DateField from '../../Editor/Fields/FieldTypes/Date'
import Email from '../../Editor/Fields/FieldTypes/Email'
import LongText from '../../Editor/Fields/FieldTypes/LongText'
import ShortText from '../../Editor/Fields/FieldTypes/ShortText'

function Field(props: FieldInterface) {
    return (
        <Row className='m-2 p-2'>
            <Col>
                <Row>
                    <Col>
                        <FormLabel>{props.name}</FormLabel>
                    </Col>
                </Row>
                {
                    props.description &&
                    <Row>
                        <Col>

                            <Form.Text>
                                {props.description}
                            </Form.Text>

                        </Col>
                    </Row>
                }
                <Row>
                    <Col>
                        {
                            (() => {
                                switch (props.type) {
                                    case FieldTypes.Checkbox:
                                        return <Checkbox {...props} />;
                                    case FieldTypes.Date:
                                        return <DateField {...props} />;
                                    case FieldTypes.Email:
                                        return <Email {...props} />;
                                    case FieldTypes.LongText:
                                        return <LongText {...props} />;
                                    case FieldTypes.ShortText:
                                        return <ShortText {...props} />;
                                    default:
                                        return <></>;
                                }
                            })()
                        }
                    </Col>
                </Row>
            </Col>
        </Row>
    )
}

export default Field