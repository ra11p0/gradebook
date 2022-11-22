import _ from 'lodash'
import React from 'react'

type Props = {
    name: string,
    formik: {
        errors: any,
        touched: any
    },
    showDespiteTouching?: boolean,
}
function FormikValidationLabel(props: Props) {
    return (
        <>
            {
                ((_.get(props.formik.errors, props.name)) &&
                    (props.showDespiteTouching || (_.get(props.formik.touched, props.name)))) ?
                    (
                        <div className="invalid-feedback d-block">{typeof (_.get(props.formik.errors, props.name)) == 'string' ? _.get(props.formik.errors, props.name) : <></>}</div>
                    ) :
                    <></>
            }
        </>
    )
}

export default FormikValidationLabel