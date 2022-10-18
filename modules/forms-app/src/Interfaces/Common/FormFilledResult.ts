export default interface FormFilledResult {
    answers: {
        uuid: string;
        answer: any;
        hasError: boolean;
    }[],
    anyHasError: boolean
}