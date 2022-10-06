import Field from "../../Interfaces/Common/Field";



export default (state: any): Field[] => {
    return (state.common.fields as Array<any>).sort((a, b) => a.order - b.order);
};
