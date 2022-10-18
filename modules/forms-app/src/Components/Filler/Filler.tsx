import { useEffect, useState } from 'react'
import { Button } from 'react-bootstrap'
import { useTranslation } from 'react-i18next'
import { connect } from 'react-redux'
import ApplicationModes from '../../Constraints/ApplicationModes'
import ValidationTypes from '../../Constraints/ValidationTypes'
import FieldInterface from '../../Interfaces/Common/Field'
import FormFilledResult from '../../Interfaces/Common/FormFilledResult'
import ReduxGetAnswers from '../../Redux/ReduxGet/ReduxGetAnswers'
import ReduxGetFields from '../../Redux/ReduxGet/ReduxGetFields'
import ReduxSetAnswer from '../../Redux/ReduxSet/ReduxSetAnswer'
import ReduxSetAnswers from '../../Redux/ReduxSet/ReduxSetAnswers'
import { store } from '../../Redux/store'
import Field from './Fields/Field'

type Props = {
    fields: FieldInterface[],
    answers?: FieldInterface[],
    onSubmit: (result: FormFilledResult) => void;
    mode: ApplicationModes;
}

function Filler(props: Props) {
    useEffect(() => {
        ReduxSetAnswers(props.answers ?? props.fields);
    }, [props.fields])
    const [validationKey, setValidationKey] = useState(ValidationTypes.InitialValidate);
    const { t } = useTranslation();
    const onBlur = (evt: { target: FieldInterface, updatedValue: any, errors: any }) => {
        const { target, updatedValue, errors } = evt;
        ReduxSetAnswer({ ...target, value: updatedValue, hasError: Object.keys(errors).length != 0 })
    }
    return (
        <div>
            <div>
                {
                    props.fields.map((field, index) => {
                        return <Field {...field} key={index} validationKey={validationKey} onBlur={onBlur} />
                    })
                }
            </div>
            {
                props.mode == ApplicationModes.Fill &&
                <div>
                    <Button
                        onClick={() => {
                            setValidationKey(ValidationTypes.SubmitValidate);
                            const fields = ReduxGetAnswers(store.getState());
                            console.dir(fields);
                            props.onSubmit({
                                answers: fields.map(field => ({ uuid: field.uuid, answer: field.value, hasError: field.hasError ?? true })),
                                anyHasError: fields.filter(field => field.hasError ?? true).length != 0 ?? true
                            })
                        }}>
                        {t('submitForm')}
                    </Button>
                </div>
            }
        </div >
    )
}

export default connect(
    (state) => ({
        fields: ReduxGetFields(state)
    }),
    () => ({})
)(Filler)