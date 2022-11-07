import pl from "date-fns/locale/pl";
import enUS from "date-fns/locale/en-US";
import moment from "moment";
import { registerLocale } from "react-datepicker";
import { SET_LANGUAGE } from "../../Constraints/actionTypes";

const setLanguage = {
    type: SET_LANGUAGE,
};
export default (dispatch: any, language: string) => {
    dispatch({ ...setLanguage, language });
    switch (language) {
        case 'pl':
            moment.locale('pl');
            registerLocale("pl", pl);
            break;
        case 'en':
            moment.locale('en');
            registerLocale("en", enUS);
            break;
    }
}
