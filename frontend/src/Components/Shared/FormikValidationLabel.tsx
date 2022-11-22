import React from 'react'

type Props = {
    name: string,
    formik: {
        errors: any,
        touched: any
    },
    showDespiteTouching?: boolean
}

function FormikValidationLabel(props: Props) {
    return (

        <>{
            (props.formik.errors[props.name] &&
                (props.showDespiteTouching || props.formik.touched[props.name])) ?
                (<div className="invalid-feedback d-block">{props.formik.errors[props.name]}</div>) :
                ''
        }</>
    )
}

export default FormikValidationLabel