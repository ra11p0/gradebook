import FieldTypes from "../../Constraints/FieldTypes";

export default interface Field {
    uuid: string;
    name: string;
    type?: FieldTypes;
    value?: any;
    labels?: string[];
    description?: string;
    distinctValues?: boolean;
    forbidPast?: boolean;
    forbidFuture?: boolean;
    isRequired?: boolean;
    hasError?: boolean;
    validationKey?: number;
    onBlur?: (evt: {
        target: Field;
        updatedValue: any;
        errors: any;
    }) => void;
}