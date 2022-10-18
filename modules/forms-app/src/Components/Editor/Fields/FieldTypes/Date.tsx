import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import ReactDatePicker from "react-datepicker";
import { useTranslation } from "react-i18next";
import ValidationTypes from "../../../../Constraints/ValidationTypes";
import Field from "../../../../Interfaces/Common/Field";

function DateField(props: Field) {
    const now = new Date();
    const [value, setValue] = useState((props.value as Date) ?? now);
    const { t } = useTranslation();
    const fieldInfo = ((forbidPast, forbidFuture) => {
        if (forbidPast && forbidFuture) { return t('dateChangeDisabled'); }
        else if (forbidFuture) { return t('dateOnlyPast'); }
        else if (forbidPast) { return t('dateOnlyFuture'); }
        else return null;
    })(props.forbidPast, props.forbidFuture);
    useEffect(() => {
        if (props.validationKey == ValidationTypes.InitialValidate)
            if (props.onBlur) props.onBlur({ target: props, updatedValue: value.toDateString(), errors: {} })
    }, [props.validationKey])

    return (
        <>
            {
                (props.forbidFuture || props.forbidPast) &&
                <Form.Text>
                    {fieldInfo}
                </Form.Text>
            }
            <ReactDatePicker
                selected={new Date(value)}
                readOnly={props.forbidFuture && props.forbidPast}
                maxDate={props.forbidFuture ? now : null}
                minDate={props.forbidPast ? now : null}
                onChange={(newValue) => {
                    setValue(newValue ?? now);
                }}
                onCalendarClose={() => { if (props.onBlur) props.onBlur({ target: props, updatedValue: value.toDateString(), errors: {} }) }}
            />
        </>
    );
}

export default DateField;
