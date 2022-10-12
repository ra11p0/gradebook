import FieldTypes from "../../Constraints/FieldTypes";

export default interface Field {
    uuid: string;
    name: string;
    type?: FieldTypes;
    value?: any;
    labels?: string[];
    description?: string;
    distinctValues?: boolean;
}